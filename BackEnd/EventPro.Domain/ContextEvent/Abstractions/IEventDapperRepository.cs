using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface IEventDapperRepository
{
    Task<IEnumerable<Event>> GetEvents();
    Task<Event?> GetEventById(int eventId); 
}