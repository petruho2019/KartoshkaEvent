using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook
{
    public class AuthorizationDetailsDto
    {
        [JsonPropertyName("rrn")]
        public string Rrn { get; set; }

        [JsonPropertyName("auth_code")]
        public string AuthCode { get; set; }

        [JsonPropertyName("three_d_secure")]
        public ThreeDSecureDto ThreeDSecure { get; set; }
    }
}