using Newtonsoft.Json;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket
{
    public class RequestPaymentObjectDto
    {
        [JsonProperty("amount")]
        public AmountDto Amount { get; set; }

        [JsonProperty("confirmation")]
        public ConfirmationDto Confirmation { get; set; }

        [JsonProperty("capture")]
        public bool Capture { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("receipt")]
        public ReceiptDto Receipt { get; set; }
    }
}
