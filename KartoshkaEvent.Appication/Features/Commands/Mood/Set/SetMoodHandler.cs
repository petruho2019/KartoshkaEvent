using KartoshkaEvent.Application.Contracts.Models.Dtos.Mood.Set;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace KartoshkaEvent.Application.Features.Commands.Mood.SetMood
{
    public class SetMoodHandler(
        ICacheService cacheService,
        IKartoshkaEventContext context,
        IEventValidationService eventValidationService) : IRequestHandler<SetMoodCommand, Result>
    {
        public async Task<Result> Handle(SetMoodCommand request, CancellationToken ct)
        {
            var @event = await context
                .Events
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TimeOfEvent)
                .Where(e => e.Id.Equals(request.EventId) && e.LocationsOfEvents.Where(l => l.Id.Equals(request.AddressId)).Any())
                .FirstOrDefaultAsync(ct);

            var eventValidationResult = eventValidationService.ExistEvent(@event!);
            if (!eventValidationResult.IsSuccess)
                return Result.FromError(eventValidationResult.Error!);

            var location = @event!.LocationsOfEvents.Where(l => l.Id.Equals(request.AddressId)).FirstOrDefault();

            var cacheKey = $"moods:{request.EventId}";
            var cacheDto = await cacheService.GetAsync<MoodCacheDto>(cacheKey, ct);

            if (cacheDto != null)
            {
                await UpdateMoodCacheAsync(request, location!, cacheDto, ct);
                return Result.NoContent();
            }
            else
            {
                location!.Mood = request.Mood;
                return Result.NoContent();
            }
        }

        private async Task UpdateMoodCacheAsync(SetMoodCommand request, Location location, MoodCacheDto cacheDto, CancellationToken ct)
        {
            var modsByAddressId = cacheDto.MoodsByLocationId.GetValueOrDefault(location!.Id) ?? throw new ArgumentNullException(message: "Кеш дал null, хотя не должен", new());
            modsByAddressId.TryGetValue(location.Mood!, out int quantityOfMoodSets);

            modsByAddressId = cacheDto.AddMoodVote(quantityOfMoodSets, request.Mood, modsByAddressId);
            
            cacheDto.MoodsByLocationId[request.AddressId] = modsByAddressId;

            await cacheService.SetAsync($"moods:{request.EventId}", cacheDto, TimeSpan.FromSeconds((location.TimeOfEvent.DateEnd - location.TimeOfEvent.DateStart).Seconds), ct);

            var moodNameWithMaxQuantity = modsByAddressId
            .OrderByDescending(k => k.Value)
            .First().Key;

            location.Mood = moodNameWithMaxQuantity;
            await context.SaveChangesAsync(ct);
        }
    }
}
