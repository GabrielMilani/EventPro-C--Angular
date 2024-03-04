using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.EventContext.Entities;
using EventPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Infrastructure.Context.EventContext.Repositories;

public class SocialNetworkRepository : ISocialNetworkRepository
{
    private readonly AppDbContext _context;

    public SocialNetworkRepository(AppDbContext context)
        => _context = context;

    public async Task<IEnumerable<SocialNetwork>> GetSocialNetworkAll()
    {
        var socialNetworkList = await _context.SocialNetworks.ToListAsync();
        return socialNetworkList ?? Enumerable.Empty<SocialNetwork>();
    }

    public async Task<SocialNetwork> GetSocialNetworkById(int socialNetworkId)
    {
        var socialNetwork = await _context.SocialNetworks.FindAsync(socialNetworkId);
        if (socialNetwork is null)
            throw new InvalidOperationException("SocialNetwork not found");
        return socialNetwork;
    }

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

    public async Task<SocialNetwork> DeleteSocialNetwork(int socialNetworkId)
    {
        var socialNetwork = await GetSocialNetworkById(socialNetworkId);
        if (socialNetwork is null)
            throw new InvalidOperationException("SocialNetwork not found");
        _context.SocialNetworks.Remove(socialNetwork);
        return socialNetwork;
    }
}