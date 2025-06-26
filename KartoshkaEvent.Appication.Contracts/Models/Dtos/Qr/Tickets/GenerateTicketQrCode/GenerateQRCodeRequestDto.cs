using KartoshkaEvent.Application.Contracts.Models.Dtos.Qr;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.GenerateQRCode
{
    public class GenerateQRCodeRequestDto
    {
        public QrInfoDto QrInfo { get; set; }
    }
}
