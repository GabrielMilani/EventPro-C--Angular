using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetEvents();
    Task<Event> GetEventById(int eventId);
    Task<Event> AddEvent(Event @event);
    void UpdateEvent(Event @event);
    Task<Event> DeleteEvent(int eventId);  
}