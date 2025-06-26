using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessPayment;
using System.Text.Json.Serialization;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook
{
    public class PaymentObjectDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("amount")]
        public AmountDto Amount { get; set; }

        [JsonPropertyName("income_amount")]
        public AmountDto IncomeAmount { get; set; }

        [JsonPropertyName("refunded_amount")]
        public AmountDto RefundedAmount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("recipient")]
        public RecipientDto Recipient { get; set; }

        [JsonPropertyName("payment_method")]
        public PaymentMethodDto PaymentMethod { get; set; }

        [JsonPropertyName("captured_at")]
        public DateTime CapturedAt { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("test")]
        public bool Test { get; set; }

        [JsonPropertyName("paid")]
        public bool Paid { get; set; }

        [JsonPropertyName("refundable")]
        public bool Refundable { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        [JsonPropertyName("authorization_details")]
        public AuthorizationDetailsDto AuthorizationDetails { get; set; }
    }
}