using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Models;
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

    public async Task<PageList<Event>> GetEvents(int userId, PageParams pageParams, bool IncludeSpeaker = false)
    {
        IQueryable<Event> query = _context.Events
            .Include(e => e.Lots)
            .Include(e => e.SocialNetworks);
        if (IncludeSpeaker)
        {
            query = query
                .Include(e => e.SpeakerEvents)
                .ThenInclude(pe => pe.Speaker);
        }
        query = query.AsNoTracking()
                    .Where(x => (x.Theme.ToLower().Contains(pageParams.Term.ToLower())) && x.UserId == userId)
                    .OrderBy(e => e.Id);

        return await PageList<Event>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
    }

    public void UpdateEvent( Event @event)
    {
        if (@event is null)
            throw new ArgumentNullException(nameof(@event));

        _context.Events.Update(@event);
    }
}