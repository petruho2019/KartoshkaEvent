using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Organizer.GetMyCreatedEvents;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Queries.Events.Organizer.GetMyCreatedEvents
{
    public class GetMyCreatedEventsHandler(
        IKartoshkaEventContext context,
        CurrentUserService currentUserService) : IRequestHandler<GetMyCreatedEventsCommand, List<CreatedEventDto>>
    {
        public async Task<List<CreatedEventDto>> Handle(GetMyCreatedEventsCommand request, CancellationToken cancellationToken)
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
                        .ThenInclude(t => t.Buyer)
                .Where(e => e.Owner.Email.Equals(currentUserService.Email))
                .Select(e => new CreatedEventDto()
                {
                    Subject = e.Subject,
                    Description = e.Description,
                    EventType = e.EventType,
                    Images = e.Images.Select(i => i.ImagePath).ToList(),
                    Tags = e.Tags.Select(t => t.Name).ToList(),
                    Location = e.LocationsOfEvents.Select(l => new CreatedLocationDto()
                    {
                        City = l.City,
                        Street = l.Street,
                        NumberOfhouse = l.NumberOfhouse,
                        DateStart = l.TimeOfEvent.DateStart,
                        DateEnd = l.TimeOfEvent.DateEnd,
                        ModerationStatus = l.ModerationStatus,
                        TicketInfos = l.Tickets.Select(t => new CreatedTicketInfoDto()
                        {
                            PriceOfTicket = t.PriceByTicket,
                            TotalQuantity = t.TotalQuantity,
                            BuyerEmail = t.Buyer.Email
                        })
                        .ToList()
                    }).Where(l => l.TicketInfos.Where(t => t.TotalQuantity >= 1).Any()).ToList(),
                })
                .Where(e => e.Location.Where(l => l.TicketInfos.Any(t => t.TotalQuantity != 0)).Count() >= 1)
                .ToListAsync(cancellationToken);
        }
    }
}
