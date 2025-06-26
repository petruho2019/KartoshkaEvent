using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class SubjectChangeRule : IRequiresModerationEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            oldEvent.Subject = newEvent.Subject;
        }

        public bool RequiresModeration(Event oldEvent, Event newEvent)
        {

            return string.IsNullOrEmpty(newEvent.Subject)
                ? false
                : oldEvent.Subject != newEvent.Subject;
        }
    }
}
