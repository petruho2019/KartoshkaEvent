using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;
using MediatR;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.DecreaseTicketQuantity
{
    public class DecreaseTicketQuantityCommand : IRequest
    {
        public YooKassaBuyTicketNotificationDto PaymentNotification { get; set; }
    }
}
