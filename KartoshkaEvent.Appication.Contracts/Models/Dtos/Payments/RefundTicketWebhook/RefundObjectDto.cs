using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;
using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.RefundTicketWebhook
{
    public class RefundObjectDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("payment_id")]
        public Guid PaymentId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = default!;

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("amount")]
        public AmountDto Amount { get; set; } = default!;

        [JsonProperty("description")]
        public string Description { get; set; } = default!;

        [JsonProperty("refund_authorization_details")]
        public RefundAuthorizationDetailsDto RefundAuthorizationDetails { get; set; } = default!;
    }
}