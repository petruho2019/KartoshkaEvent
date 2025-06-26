using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Qr.GetInfoByQr;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Queries.Qr.Tickets.GetInfoByQr
{
    public class GetInfoByQrHandler(
        IKartoshkaEventContext context,
        IJwtProvider jwtProvider) : IRequestHandler<GetInfoByQrQuery, Result<List<QrInfoResponse>>>
    {
        public async Task<Result<List<QrInfoResponse>>> Handle(GetInfoByQrQuery request, CancellationToken ct)
        {
            var qrInfoResult = jwtProvider.GetQrInfo(request.JwtQrInfo);
            if (!qrInfoResult.IsSuccess)
                return Result<List<QrInfoResponse>>.FromError(qrInfoResult.Error!);

            var qrInfo = qrInfoResult.Success!.Data;

            if (!context.Tickets.Any(t => t.Id.Equals(qrInfo.TicketId) && t.Status == Domain.Models.TicketStatus.Active))
                return Result<List<QrInfoResponse>>.BadRequest("Билет не найден");

            return Result<List<QrInfoResponse>>.Ok(await context
                .Tickets
                .Include(t => t.Buyer)
                .Include(t => t.Event)
                .Include(t => t.EventLocation)
                    .ThenInclude(l => l.TimeOfEvent)
                .Where(t => t.Id.Equals(qrInfo.TicketId))
                .Select(t => new QrInfoResponse()
                {
                    TicketId = t.Id,
                    BuyerInfo = new() { BuyerEmail = t.Buyer.Email, BuyerNickName = t.Buyer.NickName },
                    EventInfo = new() { Subject = t.Event.Subject },
                    LocationInfo = new() { City = t.EventLocation.City, NumberOfHouse = t.EventLocation.NumberOfhouse, Street = t.EventLocation.Street },
                    TimeInfo = new() { DateEnd = t.EventLocation.TimeOfEvent.DateEnd, DateStart = t.EventLocation.TimeOfEvent.DateStart }
                }).ToListAsync(ct));
        }
    }
}
