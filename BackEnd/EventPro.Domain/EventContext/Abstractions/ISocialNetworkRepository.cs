using EventPro.Domain.EventContext.Entities;

namespace EventPro.Domain.EventContext.Abstractions;

public interface ISocialNetworkRepository
{
    Task<IEnumerable<SocialNetwork>> GetSocialNetworkAll();
    Task<SocialNetwork> GetSocialNetworkById(int socialNetworkId);
    Task<SocialNetwork> AddSocialNetwork(SocialNetwork socialNetwork);
    void UpdateSocialNetwork(SocialNetwork socialNetwork);
    Task<SocialNetwork> DeleteSocialNetwork(int socialNetworkId);
}