namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetHistory
{
    public class LocationHsitoryDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int NumberOfhouse { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public List<TicketInfoHistoryDto> TicketInfos { get; set; }
    }
}
