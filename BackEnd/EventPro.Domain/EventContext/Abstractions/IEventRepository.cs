using EventPro.Domain.EventContext.Entities;

namespace EventPro.Domain.EventContext.Abstractions;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetEventAll();
    Task<Event> GetEventById(int eventId);
    Task<Event> AddEvent(Event @event);
    void UpdateEvent(Event @event);
    Task<Event> DeleteEvent(int eventId);
}