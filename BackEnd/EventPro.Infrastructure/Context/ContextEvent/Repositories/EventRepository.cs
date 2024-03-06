using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Infrastructure.Context.ContextEvent.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context) 
        => _context = context;

    public async Task<Event> AddEvent(Event @event)
    {
        if (@event is null)
            throw new ArgumentNullException(nameof(@event));
        await _context.Events.AddAsync(@event);
        return @event;
    }

    public async Task<Event> DeleteEvent(int eventId)
    {
        var @event = await GetEventById(eventId);
        if (@event is null)
            throw new InvalidOperationException("Event not found");
        _context.Events.Remove(@event);
        return @event;
    }

    public async Task<Event> GetEventById(int eventId)
    {
        var @event = await _context.Events.FindAsync(eventId);
        if (@event is null)
            throw new InvalidOperationException("Event not found");
        return @event;
    }

    public async Task<IEnumerable<Event>> GetEvents()
    {
        var eventList = await _context.Events.ToListAsync();
        return eventList ?? Enumerable.Empty<Event>();
    }

    public void UpdateEvent(Event @event)
    {
        if (@event is null)
            throw new ArgumentNullException(nameof(@event));
        _context.Events.Update(@event);
    }
}