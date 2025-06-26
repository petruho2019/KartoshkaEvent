using KartoshkaEvent.Application.Common.Extensions;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.GenerateQRCode;
using KartoshkaEvent.Application.Features.Commands.Tickets.BuyTicket;
using KartoshkaEvent.Application.Features.Commands.Tickets.FullRefund;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.YooKassaPayment.Models.FullRefund;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController(
        IMediator mediator) : ControllerBase
    {
        [HttpPost("buy")]
        [Authorize(Roles = "Visitor")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Buy([FromBody] BuyTicketCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        }
        [HttpPost("fullRefund")]
        [Authorize(Roles = "Visitor")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> FullRefund([FromBody] FullRefundCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        }
        
    }
}
