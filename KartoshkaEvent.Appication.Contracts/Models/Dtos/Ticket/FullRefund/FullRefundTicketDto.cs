namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund
{
    public class FullRefundTicketDto
    {
        public FullRefundTicketTicketInfo TicketInfo { get; set; }
        public FullRefundTicketEventInfo EventInfo { get; set; }
        public FullRefundTicketLocationInfo LocationInfo { get; set; }
    }
}
