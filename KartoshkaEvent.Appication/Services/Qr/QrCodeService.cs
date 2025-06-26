using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.GenerateQRCode;
using KartoshkaEvent.Application.Interfaces;
using QRCoder;
using System.Drawing;

namespace KartoshkaEvent.Application.Services.Qr
{
    public class QrCodeService(
        IJwtProvider jwtProvider) : IQrCodeService
    {
        public string GenerateQRCode(GenerateQRCodeRequestDto requestDto)
        {
            var qrInfoJwt = jwtProvider.GenerateJwtQrInfo(requestDto.QrInfo);

            using var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(qrInfoJwt, QRCodeGenerator.ECCLevel.Q);

            var imgType = Base64QRCode.ImageType.Png;

            var qrCode = new Base64QRCode(qrCodeData);

            string qrCodeImageAsBase64 = qrCode.GetGraphic(20, Color.Black, Color.White, true, imgType);

            return qrCodeImageAsBase64;
        }
    }
}
