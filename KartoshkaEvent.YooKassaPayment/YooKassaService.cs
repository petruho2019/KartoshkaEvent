using AutoMapper;
using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.FullRefund;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessPayment;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.Buy;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.YooKassaPayment.Models.FullRefund;
using KartoshkaEvent.YooKassaPayment.Models.Payments;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace KartoshkaEvent.YooKassaPayment
{
    public class YooKassaService : IYooKassaService
    {
        private readonly string _shopId;
        private readonly string _secretKey;
        private readonly HttpClient _httpClient;
        private readonly IMessageTemplateService _templateService;
        private readonly IMapper _mapper;
        public string Authorization { get; }

        public YooKassaService(string shopId, string apiKey, IMessageTemplateService templateService, IMapper mapper)
        {
            _shopId = shopId;
            _secretKey = apiKey;
            Authorization = $"{shopId}:{apiKey}";
            _httpClient = new();
            _templateService = templateService;
            _mapper = mapper;
        }

        public async Task<PaymentDto> BuyTicket(TicketPaymentObject ticketPaymentObject)
        {
            var paymentDescription = _templateService.MakePaymentDescription(ticketPaymentObject);

            var response = await PostJsonAsync<RequestPaymentObjectDto, Payment>("https://api.yookassa.ru/v3/payments", new()
            {
                Capture = true,
                Amount = new()
                {
                    Currency = "RUB",
                    Value = $"{(ticketPaymentObject.QuantityOfTickets * ticketPaymentObject.PriceOfTicket).ToString("F2", CultureInfo.InvariantCulture)}"
                },
                Confirmation = new()
                {
                    Type = "redirect",
                    ReturnUrl = "https://ru.wikipedia.org"
                },
                Description = "Всем привет!",
                Receipt = new()
                {
                    Customer = new() { Email = ticketPaymentObject.BuyerEmail },
                    Items = [
                        new() { Amount = new()
                            {
                                Currency = "RUB",
                                Value = $"{(ticketPaymentObject.QuantityOfTickets * ticketPaymentObject.PriceOfTicket).ToString("F2", CultureInfo.InvariantCulture)}"
                            }, 
                            Quantity = ticketPaymentObject.QuantityOfTickets, 
                            Description = paymentDescription
                        }
                        ]
                }
            });

            return _mapper.Map<PaymentDto>(response);
        }

        public async Task<FullRefundDto> FullRefundTicket(FullRefundTicketDto refundTicketDto)
        {
            var refundDescription = _templateService.MakeFullRefundTicketDescription(refundTicketDto);

            var refundResponse = await PostJsonAsync<RequestFullRefundDto, FullRefund>("https://api.yookassa.ru/v3/refunds", new()
            {
                Description = refundDescription,
                Amount = new()
                {
                    Currency = "RUB",
                    Value = $"{refundTicketDto.TicketInfo.TotalPrice.ToString("F2", CultureInfo.InvariantCulture)}"
                },
                PaymentId = refundTicketDto.TicketInfo.PaymentId
            });

            return _mapper.Map<FullRefundDto>(refundResponse);
        }

        private void AddAuthorizationHeader(HttpRequestMessage request)
        {
            if (!string.IsNullOrEmpty(Authorization))
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(Authorization));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            }
        }
        private async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string url, TRequest paymentObject)
        {
            var json = JsonConvert.SerializeObject(paymentObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            AddAuthorizationHeader(request);

            var idempotenceKey = Guid.NewGuid().ToString();
            request.Headers.Add("Idempotence-Key", idempotenceKey);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseBody) ?? Activator.CreateInstance<TResponse>();
        }
    }
}
