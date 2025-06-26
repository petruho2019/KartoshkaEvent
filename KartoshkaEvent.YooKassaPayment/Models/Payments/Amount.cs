using Newtonsoft.Json;

namespace KartoshkaEvent.YooKassaPayment.Models.Payments
{
    public class Amount
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
