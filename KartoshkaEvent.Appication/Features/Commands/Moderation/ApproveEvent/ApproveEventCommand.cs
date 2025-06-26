using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Moderation.ApproveEvent
{
    public class ApproveEventCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Идентификатор мероприятия обязателен")]
        public Guid EventId { get; set; }

        [Required(ErrorMessage = "Идентификатор адреса обязателен")]
        public Guid AddressId { get; set; }

    }
}
