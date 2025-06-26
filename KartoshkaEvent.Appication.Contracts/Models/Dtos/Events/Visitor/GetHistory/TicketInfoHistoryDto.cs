using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetHistory
{
    public class TicketInfoHistoryDto
    {
        public Guid TicketId { get; set; }
        public long TotalQuantity { get; set; }
        public decimal PriceOfTicket { get; set; }
        public string Status { get; set; }
    }
}
