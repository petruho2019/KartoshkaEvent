using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.RefundTicketWebhook;
using KartoshkaEvent.Application.Features.Commands.Tickets.AddSoldTicketToHistory;
using KartoshkaEvent.Application.Features.Commands.Tickets.DecreaseTicketQuantity;
using KartoshkaEvent.Application.Features.Commands.Tickets.RevertTicketPurchase;
using KartoshkaEvent.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(
        IMediator mediator,
        IEmailNotificationService notificationService): ControllerBase
    {

        [HttpPost("buyTicketWebhook")]
        public async Task ProcessBuyTicketWebhook([FromBody] YooKassaBuyTicketNotificationDto notificationDto)
        {
            if (notificationDto.Event.Equals("payment.succeeded"))
            {
                await mediator.Send(new DecreaseTicketQuantityCommand() { PaymentNotification = notificationDto });

                await mediator.Send(new AddSoldTicketToHistoryCommand() { PaymentNotification = notificationDto });

                await notificationService.SendSucceededPaymentNotificationAsync(notificationDto, default);

                await notificationService.SendSoldTicketOrganizerNotification(notificationDto, default);
            }
            else if (notificationDto.Event.Equals("payment.canceled"))
            {
                await notificationService.SendCanceledNotification(notificationDto, default);

                await notificationService.SendCanceledOrganizerNotification(notificationDto, default);
            }
        }

        [HttpPost("refundTicketWebhook")]
        public async Task ProcessFullRefundTicketWebhook([FromBody] YooKassaRefundTicketNotificationDto notificationDto)
        {
            if (notificationDto.Event.Equals("refund.succeeded"))
            {
                await mediator.Send(new RevertTicketPurchaseCommand() { NotificationDto = notificationDto });

                await notificationService.SendSucceededRefundTicketNotification(notificationDto, default);
                await notificationService.SendFullRefundTicketOrganizerNotification(notificationDto, default);

            }
        }
    }


}
