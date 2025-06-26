using Newtonsoft.Json;

namespace KartoshkaEvent.YooKassaPayment.Models.FullRefund
{
    public class RefundAuthorizationDetails
    {
        [JsonProperty("rrn")]
        public string Rrn { get; set; } = default!;
    }
}