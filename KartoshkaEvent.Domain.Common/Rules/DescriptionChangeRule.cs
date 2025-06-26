using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class DescriptionChangeRule : IEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            oldEvent.Description = newEvent.Description;
        }
    }
}
