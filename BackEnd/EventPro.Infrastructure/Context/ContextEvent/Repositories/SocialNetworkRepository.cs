using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Infrastructure.Context.ContextEvent.Repositories;

public class SocialNetworkRepository : ISocialNetworkRepository
{
    private readonly AppDbContext _context;

    public SocialNetworkRepository(AppDbContext context)
        => _context = context;

    public async Task<SocialNetwork> AddSocialNetwork(SocialNetwork socialNetwork)
    {
        if (socialNetwork is null)
            throw new ArgumentNullException(nameof(socialNetwork));
        await _context.SocialNetworks.AddAsync(socialNetwork);

        return socialNetwork;
    }

    public void UpdateSocialNetwork(SocialNetwork socialNetwork)
    {
        if (socialNetwork is null)
            throw new ArgumentNullException(nameof(socialNetwork));
        _context.SocialNetworks.Update(socialNetwork);
    }

    public async Task<bool> DeleteSocialNetworkSpeakerId(int speakerId, int id)
    {
        var socialNetwork = await GetSocialNetworkSpeakerByIds(speakerId, id);
        if (socialNetwork is null)
            throw new InvalidOperationException("SocialNetwork not found");
        _context.SocialNetworks.Remove(socialNetwork);
        return true;
    }
    public async Task<bool> DeleteSocialNetworkEventId(int eventId, int id)
    {
        var socialNetwork = await GetSocialNetworkEventByIds(eventId, id);
        if (socialNetwork is null)
            throw new InvalidOperationException("SocialNetwork not found");
        _context.SocialNetworks.Remove(socialNetwork);
        return true;
    }


    public async Task<SocialNetwork> GetSocialNetworkEventByIds(int eventId, int id)
    {
        IQueryable<SocialNetwork> query = _context.SocialNetworks;

        query = query.AsNoTracking()
            .Where(sn => sn.EventId == eventId &&
                         sn.Id == id);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<SocialNetwork> GetSocialNetworkSpeakerByIds(int speakerId, int id)
    {
        IQueryable<SocialNetwork> query = _context.SocialNetworks;

        query = query.AsNoTracking()
            .Where(sn => sn.SpeakerId == speakerId &&
                         sn.Id == id);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<SocialNetwork[]> GetSocialNetworksByEventId(int eventId)
    {
        IQueryable<SocialNetwork> query = _context.SocialNetworks;

        query = query.AsNoTracking()
            .Where(sn => sn.EventId == eventId);

        return await query.ToArrayAsync();
    }

    public async Task<SocialNetwork[]> GetSocialNetworksBySpeakerId(int speakerId)
    {
        IQueryable<SocialNetwork> query = _context.SocialNetworks;

        query = query.AsNoTracking()
            .Where(sn => sn.SpeakerId == speakerId);

        return await query.ToArrayAsync();
    }

}