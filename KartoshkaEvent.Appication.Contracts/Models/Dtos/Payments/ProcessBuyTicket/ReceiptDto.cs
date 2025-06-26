using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket
{
    public class ReceiptDto
    {
        [JsonProperty("customer")]
        public CustomerDto Customer { get; set; }

        [JsonProperty("items")]
        public List<ReceiptItemDto> Items { get; set; }
    }
}
