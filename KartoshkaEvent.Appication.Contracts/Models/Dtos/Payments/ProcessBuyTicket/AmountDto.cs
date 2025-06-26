using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket
{
    public class AmountDto
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
