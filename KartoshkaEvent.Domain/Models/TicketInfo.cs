namespace KartoshkaEvent.Domain.Models
{
    public record TicketInfo
    {
        public Guid Id { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public Location Address { get; set; }
    }
}
