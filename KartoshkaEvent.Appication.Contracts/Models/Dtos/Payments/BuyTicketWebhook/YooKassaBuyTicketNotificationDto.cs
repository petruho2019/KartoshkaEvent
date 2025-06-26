using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook
{
    public class YooKassaBuyTicketNotificationDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("object")]
        public PaymentObjectDto Object { get; set; }
    }
}
