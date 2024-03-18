using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Models;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface IEventRepository
{
    Task<PageList<Event>> GetEvents(int userId, PageParams pageParams, bool IncludeSpeaker = false);
    Task<Event> GetEventById(int userId, int eventId);
    Task<Event> AddEvent(Event @event);
    void UpdateEvent(Event @event);
    Task<Event> DeleteEvent(int userId, int eventId);  
}