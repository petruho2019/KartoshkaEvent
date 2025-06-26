namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Moderation.Events.GetAllOnModeration
{
    public class EventLocationOnModerationDto
    {
        public Guid AddressId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int NumberOfHouse { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal PriceOfTicket { get; set; }
        public long QuantityOfTickets { get; set; }
    }
}
