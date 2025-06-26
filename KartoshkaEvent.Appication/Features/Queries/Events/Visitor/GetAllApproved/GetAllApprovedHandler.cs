using AutoMapper;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetAllApproved;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Queries.Events.Visitor.GetAllApproved
{
    public class GetAllApprovedHandler(
        IKartoshkaEventContext context,
        IMapper mapper) : IRequestHandler<GetAllApprovedCommand, List<ApprovedEventDto>>
    {
        public async Task<List<ApprovedEventDto>> Handle(GetAllApprovedCommand request, CancellationToken ct)
        {
            var approvedEvents = await context
                .Events
                .Include(e => e.Images)
                .Include(e => e.Tags)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TicketInfo)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.Tickets)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TimeOfEvent)
                .Select(e => new ApprovedEventDto
                {
                    Id = e.Id,
                    Subject = e.Subject,
                    Description = e.Description,
                    EventType = e.EventType.ToString(),
                    OwnerId = e.OwnerId,
                    Images = e.Images.Select(i => i.ImagePath).ToList(),
                    Tags = e.Tags.Select(t => t.Name).ToList(),
                    Locations = e.LocationsOfEvents.Where(l => l.ModerationStatus.Equals(ModerationStatus.Approved))
                        .Select(ae => new ApprovedLocationDto()
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
                .Where(a => a.Locations.Count > 0)
                .ToListAsync(ct);

            return approvedEvents;
        }
    }
}
