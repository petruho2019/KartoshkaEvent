using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetHistory;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Events.Visitor.GetHistory
{
    public record GetHistoryQuery : IRequest<List<EventHistoryDto>>;
}
