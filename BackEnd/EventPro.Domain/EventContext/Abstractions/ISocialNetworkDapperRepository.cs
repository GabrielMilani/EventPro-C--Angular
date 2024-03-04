using EventPro.Domain.EventContext.Entities;

namespace EventPro.Domain.EventContext.Abstractions;

public interface ISocialNetworkDapperRepository
{
    Task<IEnumerable<SocialNetwork>> GetSocialNetworks();
    Task<SocialNetwork?> GetSocialNetworkById(int socialNetworkId);   
}