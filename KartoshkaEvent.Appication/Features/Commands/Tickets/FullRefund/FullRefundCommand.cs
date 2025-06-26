using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.FullRefund
{
    public class FullRefundCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Идентификатор билета не может быть пустым")]
        public Guid TicketId { get; set; }
    }
}
