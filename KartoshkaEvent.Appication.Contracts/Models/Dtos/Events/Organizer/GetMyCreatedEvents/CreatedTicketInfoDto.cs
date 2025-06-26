namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Organizer.GetMyCreatedEvents
{
    public class CreatedTicketInfoDto
    {
        public long TotalQuantity { get; set; }
        public decimal PriceOfTicket { get; set; }

        public string BuyerEmail { get; set; }
    }
}
