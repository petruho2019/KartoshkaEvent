using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.RefundTicketWebhook
{
    public class RefundAuthorizationDetailsDto
    {
        [JsonProperty("rrn")]
        public string Rrn { get; set; } = default!;
    }
}