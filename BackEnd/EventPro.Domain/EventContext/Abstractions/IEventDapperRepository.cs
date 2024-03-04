using EventPro.Domain.EventContext.Entities;

namespace EventPro.Domain.EventContext.Abstractions;

public interface IEventDapperRepository
{
    Task<IEnumerable<Event>> GetEvents();
    Task<Event?> GetEventById(int eventId); 
}