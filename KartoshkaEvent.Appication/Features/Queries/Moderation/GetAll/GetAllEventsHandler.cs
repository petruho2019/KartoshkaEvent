using KartoshkaEvent.Application.Contracts.Models.Dtos.Moderation.Events.GetAllForModeration;
using KartoshkaEvent.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Queries.Moderation.GetAll
{
    public class GetAllEventsHandler(
        IKartoshkaEventContext context) : IRequestHandler<GetAllEventsQuery, List<ModerationEventDto>>
    {
        public async Task<List<ModerationEventDto>> Handle(GetAllEventsQuery request, CancellationToken ct)
        {
            return await context.Events
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(a => a.TimeOfEvent)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(a => a.TicketInfo)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(a => a.Tickets)
                .Select(e => new ModerationEventDto
                {
                    Id = e.Id,
                    Subject = e.Subject,
                    Description = e.Description,
                    EventType = e.EventType.ToString(),
                    OwnerId = e.OwnerId,
                    Images = e.Images.Select(i => i.ImagePath).ToList(),
                    Tags = e.Tags.Select(t => t.Name).ToList(),
                    Addresses = e.LocationsOfEvents
                        .Select(ae => new ModerationLocationEventDto()
                        {
                            AddressId = ae.Id,
                            City = ae.City,
                            Street = ae.Street,
                            DateStart = ae.TimeOfEvent.DateStart,
                            DateEnd = ae.TimeOfEvent.DateEnd,
                            NumberOfHouse = ae.NumberOfhouse,
                            PriceOfTicket = ae.TicketInfo.Price,
                            QuantityOfTickets = ae.TicketInfo.Quantity,
                            ModerationStatus = ae.ModerationStatus.ToString(),
                            SoldTickets = ae.Tickets.Sum(t => t.TotalQuantity)
                        })
                        .ToList()
                })
                .ToListAsync(ct);
        }
    }
}
