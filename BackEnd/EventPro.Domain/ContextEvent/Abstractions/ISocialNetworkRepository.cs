using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ISocialNetworkRepository
{
    Task<IEnumerable<SocialNetwork>> GetSocialNetworks();
    Task<SocialNetwork> GetSocialNetworkById(int socialNetworkId);
    Task<SocialNetwork> AddSocialNetwork(SocialNetwork socialNetwork);
    void UpdateSocialNetwork(SocialNetwork socialNetwork);
    Task<SocialNetwork> DeleteSocialNetwork(int socialNetworkId);
}