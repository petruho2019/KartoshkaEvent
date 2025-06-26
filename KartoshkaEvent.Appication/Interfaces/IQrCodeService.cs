using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.GenerateQRCode;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface IQrCodeService
    {
        string GenerateQRCode(GenerateQRCodeRequestDto requestDto);
    }
}
