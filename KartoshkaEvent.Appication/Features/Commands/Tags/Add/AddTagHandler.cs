using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Commands.Tags.Add
{
    public class AddTagHandler(
        IKartoshkaEventContext context) : IRequestHandler<AddTagCommand, Result>
    {
        public async Task<Result> Handle(AddTagCommand request, CancellationToken ct)
        {
            if (await context.Tags.AnyAsync(t => t.Name.Equals(request.Name), ct))
                return Result.BadRequest("Тег с этим именем уже есть");

            var tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await context.Tags.AddAsync(tag, ct);
            await context.SaveChangesAsync(ct);

            return Result.Created();
        }
    }
}
