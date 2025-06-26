using AutoMapper;
using AutoMapper.QueryableExtensions;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Moderation.Events.GetAllOnModeration;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Queries.Moderation.GetAll
{
    public class GetEventsOnModerationHandler(
        IKartoshkaEventContext context,
        IMapper mapper) : IRequestHandler<GetEventsOnModerationQuery, List<EventOnModerationDto>>
    {
        public async Task<List<EventOnModerationDto>> Handle(GetEventsOnModerationQuery request, CancellationToken ct)
        {
            return await context.Events
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(a => a.TimeOfEvent)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(a => a.TicketInfo)
                .Where(e => e.LocationsOfEvents.Any(a => a.ModerationStatus == ModerationStatus.OnModeration))
                .Select(e => new EventOnModerationDto
                {
                    Id = e.Id,
                    Subject = e.Subject,
                    Description = e.Description,
                    EventType = e.EventType.ToString(),
                    OwnerId = e.OwnerId,
                    Images = e.Images.Select(i => i.ImagePath).ToList(),
                    Tags = e.Tags.Select(t => t.Name).ToList(),
                    Addresses = e.LocationsOfEvents
                        .Where(a => a.ModerationStatus == ModerationStatus.OnModeration)
                        .Select(ae => new EventLocationOnModerationDto
                        {
                            AddressId = ae.Id,
                            City = ae.City,
                            Street = ae.Street,
                            DateStart = ae.TimeOfEvent.DateStart,
                            DateEnd = ae.TimeOfEvent.DateEnd,
                            NumberOfHouse = ae.NumberOfhouse,
                            PriceOfTicket = ae.TicketInfo.Price,
                            QuantityOfTickets = ae.TicketInfo.Quantity
                        })
                        .ToList()
                })
                .ToListAsync(ct);
        }
    }
}
