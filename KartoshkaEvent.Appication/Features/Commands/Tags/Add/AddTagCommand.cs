using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Tags.Add
{
    public record AddTagCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Название тега обязательно")]
        public string Name { get; set; }
    }
}
