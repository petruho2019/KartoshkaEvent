using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Search.GetByTags;
using KartoshkaEvent.Application.Features.Queries.Events.SearchEvents.GetByTags;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Queries.Events.Search.GetByTags
{
    public class GetByTagsHandler(
        IKartoshkaEventContext context) : IRequestHandler<GetByTagsQuery, List<EventGetByTagsDto>>
    {
        public async Task<List<EventGetByTagsDto>> Handle(GetByTagsQuery request, CancellationToken ct)
        {
            return await context
                .Events
                .Include(e => e.Images)
                .Include(e => e.Tags)
                .Include(e => e.Owner)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TicketInfo)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.Tickets)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TimeOfEvent)
                .Where(e => e.Tags.Any(t => request.Tags.Any(tagname => t.Name == tagname)))
                .Select(e => new EventGetByTagsDto
                {
                    Id = e.Id,
                    Subject = e.Subject,
                    Description = e.Description,
                    EventType = e.EventType.ToString(),
                    OwnerInfo = new() { Email = e.Owner.Email, NickName = e.Owner.NickName },
                    Images = e.Images.Select(i => i.ImagePath).ToList(),
                    Tags = e.Tags.Select(t => t.Name).ToList(),
                    Locations = e.LocationsOfEvents.Where(l => l.ModerationStatus.Equals(ModerationStatus.Approved))
                        .Select(ae => new LocationGetByTagsDto()
                        {
                            AddressId = ae.Id,
                            City = ae.City,
                            Street = ae.Street,
                            DateStart = ae.TimeOfEvent.DateStart,
                            DateEnd = ae.TimeOfEvent.DateEnd,
                            NumberOfHouse = ae.NumberOfhouse,
                            PriceOfTicket = ae.TicketInfo.Price,
                            QuantityOfTickets = ae.TicketInfo.Quantity,
                            SoldTickets = ae.Tickets.Sum(t => t.TotalQuantity),
                            Mood = ae.Mood!
                        })
                        .ToList()
                })
                .Where(a => a.Locations.Any())
                .ToListAsync(ct);
        }
    }
}
