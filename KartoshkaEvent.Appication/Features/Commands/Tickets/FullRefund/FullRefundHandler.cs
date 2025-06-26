using AutoMapper;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund.Cache;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Application.Services;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.FullRefund
{
    public class FullRefundHandler(
        IKartoshkaEventContext context,
        IYooKassaService yooKassaService,
        IMapper mapper,
        ICacheService cacheService,
        CurrentUserService currentUserService) : IRequestHandler<FullRefundCommand, Result>
    {
        public async Task<Result> Handle(FullRefundCommand request, CancellationToken ct)
        {
            var ticket = await context.Tickets.Include(t => t.Event).Include(t => t.EventLocation).FirstOrDefaultAsync(t => t.BuyerId.Equals(currentUserService.UserId) && t.Id.Equals(request.TicketId));
            if (ticket == null)
                return Result.BadRequest("Билет не найден");

            var refundDto = mapper.Map<FullRefundTicketDto>(ticket);

            var fullRefultDto = await yooKassaService.FullRefundTicket(refundDto);

            if (fullRefultDto.Status == "succeeded")
            {
                var cacheInfo = mapper.Map<FullRefundInfoCache>(fullRefultDto);
                cacheInfo.BuyerEmail = ticket.Buyer.Email;

                await cacheService.SetAsync($"refund:{fullRefultDto.Id}", cacheInfo, TimeSpan.FromHours(3), ct);
                return Result.NoContent();
            }

            return Result.BadRequest(fullRefultDto.Description);
        }
    }
}
