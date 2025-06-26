using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;
using MediatR;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.AddSoldTicketToHistory
{
    public class AddSoldTicketToHistoryCommand : IRequest
    {
        public YooKassaBuyTicketNotificationDto PaymentNotification { get; set; }
    }
}
