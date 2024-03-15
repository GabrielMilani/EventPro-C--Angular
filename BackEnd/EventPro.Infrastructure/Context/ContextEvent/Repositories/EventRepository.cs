using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

    public async Task<Event> DeleteEvent(int userId, int eventId)
    {
        var @event = await GetEventById(userId, eventId);
        if (@event is null)
            throw new InvalidOperationException("Event not found");
        _context.Events.Remove(@event);
        return @event;
    }

    public async Task<Event> GetEventById(int userId, int eventId)
    {
        var @event = await _context.Events
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == eventId);
        if (@event is null)
            throw new InvalidOperationException("Event not found");
        return @event;
    }

    public async Task<IEnumerable<Event>> GetEvents(int userId)
    {
        var eventList = await _context.Events
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
        return eventList ?? Enumerable.Empty<Event>();
    }

    public async Task<IEnumerable<Event>> GetEventsByTheme(int userId, string theme)
    {
        var eventList = await _context.Events
            .Where(x => x.UserId == userId && x.Theme.ToLower().Contains(theme.ToLower()))
            .AsNoTracking()
            .ToListAsync();
        return eventList ?? Enumerable.Empty<Event>();
    }

    public void UpdateEvent( Event @event)
    {
        if (@event is null)
            throw new ArgumentNullException(nameof(@event));

        _context.Events.Update(@event);
    }
}