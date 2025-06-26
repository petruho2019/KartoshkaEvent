using System.Text.Json.Serialization;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook
{
    public class RecipientDto
    {
        [JsonPropertyName("account_id")]
        public int AccountId { get; set; }

        [JsonPropertyName("gateway_id")]
        public int GatewayId { get; set; }
    }
}