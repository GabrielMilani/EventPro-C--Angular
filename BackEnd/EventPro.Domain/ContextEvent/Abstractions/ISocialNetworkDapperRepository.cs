using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ISocialNetworkDapperRepository
{
    Task<IEnumerable<SocialNetwork>> GetSocialNetworks();
    Task<SocialNetwork?> GetSocialNetworkById(int socialNetworkId);
}