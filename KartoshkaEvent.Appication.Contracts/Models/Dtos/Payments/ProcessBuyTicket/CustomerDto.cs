using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket
{
    public class CustomerDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
