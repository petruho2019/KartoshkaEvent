using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook
{
    public class ThreeDSecureDto
    {
        [JsonProperty("applied")]
        public bool Applied { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("method_completed")]
        public bool MethodCompleted { get; set; }

        [JsonProperty("challenge_completed")]
        public bool ChallengeCompleted { get; set; }
    }
}