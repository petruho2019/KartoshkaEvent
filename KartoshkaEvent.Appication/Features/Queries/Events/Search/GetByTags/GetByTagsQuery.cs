using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Search.GetByTags;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Events.SearchEvents.GetByTags
{
    public class GetByTagsQuery : IRequest<List<EventGetByTagsDto>>
    {
        public List<string> Tags { get; set; }
    }
}
