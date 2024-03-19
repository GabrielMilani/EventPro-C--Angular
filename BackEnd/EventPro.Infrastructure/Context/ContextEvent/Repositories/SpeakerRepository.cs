using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextEvent.Entities.Enum;
using EventPro.Domain.ContextShared.Models;
using EventPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Infrastructure.Context.ContextEvent.Repositories;

public class SpeakerRepository : ISpeakerRepository
{
    private readonly AppDbContext _context;

    public SpeakerRepository(AppDbContext context)
        => _context = context;

    public async Task<PageList<Speaker>> GetSpeakers(PageParams pageParams, bool includeEvents = false)
    {
        IQueryable<Speaker> query = _context.Speakers
            .Include(p => p.User)
            .Include(p => p.SocialNetworks);

        if (includeEvents)
        {
            query = query
                .Include(s => s.SpeakerEvents)
                .ThenInclude(se => se.Event);
        }

        query = query.AsNoTracking()
                     .Where(s => (s.MiniCV.ToLower().Contains(pageParams.Term.ToLower()) ||
                                  s.User.FirstName.ToLower().Contains(pageParams.Term.ToLower()) ||
                                  s.User.LastName.ToLower().Contains(pageParams.Term.ToLower())) &&
                                  s.User.Function == EFunction.Palestrante)
                     .OrderBy(s => s.Id);

        return await PageList<Speaker>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
    }

    public async Task<Speaker> GetSpeakerByUserId(int userId, bool includeEvents = false)
    {
        IQueryable<Speaker> query = _context.Speakers
            .Include(s => s.User)
            .Include(s => s.SocialNetworks);

        if (includeEvents)
        {
            query = query
                .Include(s => s.SpeakerEvents)
                .ThenInclude(se => se.Event);
        }

        query = query.AsNoTracking().OrderBy(s => s.Id)
                     .Where(s => s.UserId == userId);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Speaker> AddSpeaker(Speaker speaker)
    {
        if (speaker is null)
            throw new ArgumentNullException(nameof(speaker));
        await _context.Speakers.AddAsync(speaker);
        return speaker;
    }

    public void UpdateSpeaker(Speaker speaker)
    {
        if (speaker is null)
            throw new ArgumentNullException(nameof(speaker));
        _context.Speakers.Update(speaker);
    }
}