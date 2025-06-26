using KartoshkaEvent.Application.Contracts.Models.Dtos.Qr;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Qr.GetInfoByQr;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Qr.Tickets.GetInfoByQr
{
    public class GetInfoByQrQuery : IRequest<Result<List<QrInfoResponse>>>
    {
        public string JwtQrInfo { get; set; }
    }
}
