using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Organizer.GetMyCreatedEvents
{
    public class CreatedLocationDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int NumberOfhouse { get; set; }
        public ModerationStatus ModerationStatus { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public List<CreatedTicketInfoDto> TicketInfos { get; set; }
    }
}
