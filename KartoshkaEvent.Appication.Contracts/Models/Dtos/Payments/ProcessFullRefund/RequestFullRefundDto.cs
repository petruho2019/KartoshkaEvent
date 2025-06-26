using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;
using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.FullRefund
{
    public class RequestFullRefundDto
    {
        [JsonProperty("amount")]
        public AmountDto Amount { get; set; }

        [JsonProperty("payment_id")]
        public Guid PaymentId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
