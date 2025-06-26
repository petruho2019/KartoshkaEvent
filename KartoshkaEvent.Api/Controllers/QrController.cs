using KartoshkaEvent.Application.Common.Extensions;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.GenerateQRCode;
using KartoshkaEvent.Application.Features.Queries.Qr.Tickets.GetInfoByQr;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class QrController(
        IQrCodeService qrCodeService,
        IMediator mediator) : ControllerBase
    {

        [HttpPost("generateTicketQr")]
        [Authorize(Roles = "Visitor")]
        [ProducesResponseType(typeof(Success), 200)]
        public async Task<IActionResult> GenerateQRCode([FromBody] GenerateQRCodeRequestDto requestDto)
        {
            var qrCode = qrCodeService.GenerateQRCode(requestDto);

            return Ok(qrCode);
        }

        [HttpPost("getTicketInfoByQrInfo")]
        [Authorize(Roles = "Moderator")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetInfoByQrInfo([FromBody] string jwtQrInfo)
        {
            var result = await mediator.Send(new GetInfoByQrQuery() { JwtQrInfo = jwtQrInfo });
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        }

    }
}
