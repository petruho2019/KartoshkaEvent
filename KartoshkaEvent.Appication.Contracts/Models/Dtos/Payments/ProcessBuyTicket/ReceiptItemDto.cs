using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;
using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket
{
    public class ReceiptItemDto
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("amount")]
        public AmountDto Amount { get; set; }

        [JsonProperty("vat_code")]
        public int VatCode { get; set; }
    }
}
