namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessPayment
{
    public class PaymentDto
    {
        public string PaymentId { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PaymentUrl { get; set; }
        public bool IsPaid { get; set; }
        public IDictionary<string, object> Metadata { get; set; }

    }
}
