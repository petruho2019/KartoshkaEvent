using Newtonsoft.Json;

namespace KartoshkaEvent.YooKassaPayment.Models.Payments
{
    public class Confirmation
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("confirmation_url")]
        public string ConfirmationUrl { get; set; }
    }
}