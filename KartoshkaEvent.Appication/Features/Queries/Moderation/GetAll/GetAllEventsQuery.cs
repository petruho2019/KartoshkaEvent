using KartoshkaEvent.Application.Contracts.Models.Dtos.Moderation.Events.GetAllForModeration;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Moderation.GetAll
{
    public record GetAllEventsQuery : IRequest<List<ModerationEventDto>>;
}
