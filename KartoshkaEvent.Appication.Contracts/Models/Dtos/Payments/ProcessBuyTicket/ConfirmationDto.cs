using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket
{
    public class ConfirmationDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("return_url")]
        public string ReturnUrl { get; set; }
    }
}