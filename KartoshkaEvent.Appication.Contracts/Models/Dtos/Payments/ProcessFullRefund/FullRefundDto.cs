using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;
using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.FullRefund
{
    public class FullRefundDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("payment_id")]
        public Guid PaymentId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = default!;

        [JsonProperty("amount")]
        public AmountDto Amount { get; set; } = default!;

        [JsonProperty("description")]
        public string Description { get; set; } = default!;
    }
}
