using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Qr
{
    public class QrInfoDto
    {
        [Required(ErrorMessage = "Ticket id is required!")]
        public Guid TicketId { get; set; }
    }
}
