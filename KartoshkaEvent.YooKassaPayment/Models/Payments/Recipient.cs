using Newtonsoft.Json;

namespace KartoshkaEvent.YooKassaPayment.Models.Payments
{
    public class Recipient
    {

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("gateway_id")]
        public string GatewayId { get; set; }
    }
}