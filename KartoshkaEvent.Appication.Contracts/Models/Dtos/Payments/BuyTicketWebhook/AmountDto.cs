using System.Text.Json.Serialization;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook
{
    public class AmountDto
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
