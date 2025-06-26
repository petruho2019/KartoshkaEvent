using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Interfaces
{
    public interface IEditRule
    {
        void ApplyChange(ref Event oldEvent, Event newEvent);
    }
}
