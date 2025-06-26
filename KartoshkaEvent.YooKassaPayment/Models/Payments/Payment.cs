namespace KartoshkaEvent.YooKassaPayment.Models.Payments
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;


    public class Payment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("amount")]
        public Amount Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("confirmation")]
        public Confirmation Confirmation { get; set; }

        [JsonProperty("test")]
        public bool Test { get; set; }

        [JsonProperty("paid")]
        public bool Paid { get; set; }

        [JsonProperty("refundable")]
        public bool Refundable { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }


}
