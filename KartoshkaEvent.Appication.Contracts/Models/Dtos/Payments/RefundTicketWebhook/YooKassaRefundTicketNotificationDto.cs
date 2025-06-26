using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.RefundTicketWebhook
{
    public class YooKassaRefundTicketNotificationDto
    {
        [JsonProperty("type")]
        public string Type { get; set; } = default!;

        [JsonProperty("event")]
        public string Event { get; set; } = default!;

        [JsonProperty("object")]
        public RefundObjectDto Object { get; set; } = default!;
    }
}
