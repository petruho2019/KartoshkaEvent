using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Search.BetweenDate;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Events.Search.BetweenDate
{
    public record GetEventsBetweenDateQuery : IRequest<List<EventBetweenDateDto>>
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
