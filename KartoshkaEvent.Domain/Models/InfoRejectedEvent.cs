namespace KartoshkaEvent.Domain.Models
{
    public record InfoRejectedEvent
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }

        public Guid AddressId { get; set; }
        public Location Address { get; set; }
    }
}
