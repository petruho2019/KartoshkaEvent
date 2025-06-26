using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Application.Services;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;

namespace KartoshkaEvent.Application.Features.Commands.Events.AddForModeration
{
    public class AddEventForModerationHandler(
        IKartoshkaEventContext context,
        IImageService imageService,
        CurrentUserService currentUserService,
        ITagService tagService,
        ILocationService locationService) : IRequestHandler<AddEventForModerationCommand, Result>
    {
        public async Task<Result> Handle(AddEventForModerationCommand request, CancellationToken ct)
        {
            if (request.Locations.Any(a => a.DateEnd <= a.DateStart))
                return Result.BadRequest("Дата окончания не может быть позде даты начала");

            var eventForModerationId = Guid.NewGuid();
            var @event = new Event()
            {
                Id = eventForModerationId,
                Subject = request.Subject,
                Description = request.Description!,
                EventType = Enum.Parse<EventType>(request.EventType),
                OwnerId = currentUserService.UserId,
                Tags = []
            };

            var tags = context.Tags.ToDictionary(t => t.Name);

            var validTagsResult = tagService.GetValidTags(tags, request.Tags);
            if (!validTagsResult.IsSuccess)
                return Result.FromError(validTagsResult.Error!);

            @event.Tags = validTagsResult.Success!.Data;

            var imagesBytes = await imageService.GetBytesAsync(request.Images, ct);
            @event.Images = imageService.WriteImagesAndReturn(imagesBytes, eventForModerationId);

            var addressWithTime = locationService.BuildLocationWithTicketInfoAndTimeOfEvent(eventForModerationId, request);

            await context.EventLocations.AddRangeAsync(addressWithTime, ct);
            await context.Events.AddAsync(@event, ct);
            await context.SaveChangesAsync(ct);

            return Result.Created();
        }
    }
}
