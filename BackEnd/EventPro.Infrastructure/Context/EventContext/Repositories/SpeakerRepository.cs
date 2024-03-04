using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.EventContext.Entities;
using EventPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Infrastructure.Context.EventContext.Repositories;

public class SpeakerRepository  : ISpeakerRepository
{
    private readonly AppDbContext _context;

    public SpeakerRepository(AppDbContext context)
        => _context = context;

    public async Task<IEnumerable<Speaker>> GetSpeakerAll()
    {
        var speakerList = await _context.Speakers.ToListAsync();
        return speakerList ?? Enumerable.Empty<Speaker>();
    }

    public async Task<Speaker> GetSpeakerById(int speakerId)
    {
        var speaker = await _context.Speakers.FindAsync(speakerId);
        if (speaker is null)
            throw new InvalidOperationException("Speaker not found");
        return speaker;
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

    public async Task<Speaker> DeleteSpeaker(int speakerId)
    {
        var speaker = await GetSpeakerById(speakerId);
        if (speaker is null)
            throw new InvalidOperationException("Speaker not found");
        _context.Speakers.Remove(speaker);
        return speaker;
    }
}