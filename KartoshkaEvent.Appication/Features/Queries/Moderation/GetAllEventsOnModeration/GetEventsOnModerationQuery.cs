using KartoshkaEvent.Application.Contracts.Models.Dtos.Moderation.Events.GetAllOnModeration;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Moderation.GetAll
{
    public record GetEventsOnModerationQuery : IRequest<List<EventOnModerationDto>>;
}
