using KartoshkaEvent.Domain.Common.Utils;
using MediatR;

namespace KartoshkaEvent.Application.Features.Commands.Mood.SetMood
{
    public class SetMoodCommand : IRequest<Result>
    {
        public string Mood { get; set; }
        public Guid EventId { get; set; }
        public Guid AddressId { get; set; }
    }
}
