using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.BuyTicket
{
    public class BuyTicketCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = "Идентификатор события обязателен")]
        public Guid EventId { get; set; }

        [Required(ErrorMessage = "Идентификатор адреса обязателен")]
        public Guid AddressId { get; set; }

        [Required(ErrorMessage = "Количество купленных билетов обязательно")]
        [Range(0, long.MaxValue, ErrorMessage = "Количество покупаемых билето не может быть отрицательным")]
        public long Quantity { get; set; }


    }
}
