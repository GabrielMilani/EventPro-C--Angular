using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetEvents(int userId);
    Task<Event> GetEventById(int userId, int eventId);
    Task<IEnumerable<Event>> GetEventsByTheme(int userId, string theme);
    Task<Event> AddEvent(Event @event);
    void UpdateEvent(Event @event);
    Task<Event> DeleteEvent(int userId, int eventId);  
}