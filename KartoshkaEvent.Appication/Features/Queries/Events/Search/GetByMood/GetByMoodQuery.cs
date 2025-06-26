using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Search.GetByMood;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Events.Search.GetByMood
{
    public class GetByMoodQuery : IRequest<List<EventGetByMood>>
    {
        public string Mood { get; set; }
    }
}
