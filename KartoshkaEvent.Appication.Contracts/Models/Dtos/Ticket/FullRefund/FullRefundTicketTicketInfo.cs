namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund
{
    public class FullRefundTicketTicketInfo
    {
        public decimal TotalPrice { get; set; }
        public long Quantity { get; set; }
        public Guid PaymentId { get; set; }
    }
}