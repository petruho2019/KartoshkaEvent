using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Moderation.RejectEvent
{
    public class RejectEventCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Идентификатор мероприятия обязателен")]
        public Guid EventId { get; set; }
        
        [Required(ErrorMessage = "Идентификатор адреса обязателен")]
        public Guid AddressId { get; set; }

        [Required(ErrorMessage = "Сообщение отклонения обязательно")]
        public string RejectedMessage { get; set; }
    }
}
