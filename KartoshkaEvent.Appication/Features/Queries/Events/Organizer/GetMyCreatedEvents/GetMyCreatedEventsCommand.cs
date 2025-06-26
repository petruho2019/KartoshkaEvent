using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Organizer.GetMyCreatedEvents;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Events.Organizer.GetMyCreatedEvents
{
    public record GetMyCreatedEventsCommand : IRequest<List<CreatedEventDto>>;
}
