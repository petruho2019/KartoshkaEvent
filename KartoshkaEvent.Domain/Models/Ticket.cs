namespace KartoshkaEvent.Domain.Models
{
    public record Ticket
    {
        public Guid Id { get; set; }
        public long TotalQuantity { get; set; }
        public decimal PriceByTicket { get; set; }
        public TicketStatus Status { get; set; }
        public Guid PaymentId { get; set; }

        public Guid BuyerId { get; set; }
        public User Buyer { get; set; }

        public Guid EventLocationId { get; set; }
        public Location EventLocation { get; set; }

        public Guid EventId { get; set; }
        public Event Event{ get; set; }
    }
}
