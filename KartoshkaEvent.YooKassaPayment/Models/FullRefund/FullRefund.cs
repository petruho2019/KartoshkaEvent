using KartoshkaEvent.YooKassaPayment.Models.Payments;
using Newtonsoft.Json;

namespace KartoshkaEvent.YooKassaPayment.Models.FullRefund
{
    public class FullRefund
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
        public Amount Amount { get; set; } = default!;

        [JsonProperty("description")]
        public string Description { get; set; } = default!;

        [JsonProperty("refund_authorization_details")]
        public RefundAuthorizationDetails RefundAuthorizationDetails { get; set; } = default!;
    }
}
