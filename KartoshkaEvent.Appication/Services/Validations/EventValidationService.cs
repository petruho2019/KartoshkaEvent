using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Services.Validations
{
    public class EventValidationService : IEventValidationService
    {
        public Result EnsureEditableByModerationStatus(ModerationStatus moderationStatus)
        {
            if (moderationStatus.Equals(ModerationStatus.Rejected))
                return Result.BadRequest("Нельзя изменить мероприятия которое отменено модерацией!");
            if (moderationStatus.Equals(ModerationStatus.OnModerationEdit))
                return Result.BadRequest("Нельзя изменить мероприятия которое уже находится на рассмотрении изменений модерацией!");

            return Result.NoContent();
        }

        public Result ExistEvent(Event @event)
        {
            if (@event == null)
                return Result.BadRequest("Мероприятие не найдено!");

            return Result.NoContent();
        }

        public Result ValidateNewEventTime(TimeOfEvent oldTime, TimeOfEvent newTime)
        {
            if (oldTime.DateStart - DateTime.UtcNow.AddHours(3) <= TimeSpan.FromDays(1))
                return Result.BadRequest("Нельзя редактировать меропритие за 24 часа до начала");
        
            if (newTime.DateStart == DateTime.MinValue && newTime.DateEnd == DateTime.MinValue)
                return Result.NoContent();

            if (newTime.DateEnd == DateTime.MinValue)
                return Result.NoContent();
            
            if (newTime.DateStart == DateTime.MinValue)
                return Result.NoContent();

            if (newTime.DateStart > newTime.DateEnd)
                return Result.BadRequest("Новая дата окончания не может быть позже новой даты начала");


            return Result.NoContent();
        }
    }
}
