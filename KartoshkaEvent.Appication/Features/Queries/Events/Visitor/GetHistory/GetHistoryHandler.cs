using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Organizer.GetMyCreatedEvents;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetHistory;
using KartoshkaEvent.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Queries.Events.Visitor.GetHistory
{
    public class GetHistoryHandler(
        IKartoshkaEventContext context) : IRequestHandler<GetHistoryQuery, List<EventHistoryDto>>
    {
        public async Task<List<EventHistoryDto>> Handle(GetHistoryQuery request, CancellationToken ct)
        {
            return await context
                .Events
                .Include(e => e.Owner)
                .Include(e => e.Tags)
                .Include(e => e.Images)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TimeOfEvent)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TicketInfo)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.Tickets)
                .Select(e => new EventHistoryDto()
                {
                    Subject = e.Subject,
                    Description = e.Description,
                    EventType = e.EventType,
                    Images = e.Images.Select(i => i.ImagePath).ToList(),
                    OwnerInfo = new() { OwnerEmail = e.Owner.Email, OwnerNickName = e.Owner.NickName, OwnerId = e.OwnerId },
                    Tags = e.Tags.Select(t => t.Name).ToList(),
                    Location = e.LocationsOfEvents.Select(l => new LocationHsitoryDto()
                    {
                        City = l.City,
                        Street = l.Street,
                        NumberOfhouse = l.NumberOfhouse,
                        DateStart = l.TimeOfEvent.DateStart,
                        DateEnd = l.TimeOfEvent.DateEnd,
                        TicketInfos = l.Tickets.Select(t => new TicketInfoHistoryDto() {
                            PriceOfTicket = t.PriceByTicket,
                            TotalQuantity = t.TotalQuantity,
                            TicketId = t.Id,
                            Status = t.Status.ToString()
                        })
                        .ToList()
                    }).Where(l => l.TicketInfos.Where(t => t.TotalQuantity >= 1).Any()).ToList(),
                })
                .Where(e => e.Location.Where(l => l.TicketInfos.Any(t => t.TotalQuantity != 0)).Count() >= 1)
                .ToListAsync(ct);
        }
    }
}
