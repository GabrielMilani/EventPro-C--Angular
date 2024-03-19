using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ISocialNetworkRepository
{
    Task<SocialNetwork> GetSocialNetworkEventByIds(int eventId, int id);
    Task<SocialNetwork> GetSocialNetworkSpeakerByIds(int speakerId, int id);
    
    Task<SocialNetwork[]> GetSocialNetworksByEventId(int eventId);
    Task<SocialNetwork[]> GetSocialNetworksBySpeakerId(int speakerId);

    Task<bool> DeleteSocialNetworkEventId( int eventId, int id);
    Task<bool> DeleteSocialNetworkSpeakerId(int speakerId, int id);
    
    Task<SocialNetwork> AddSocialNetwork(SocialNetwork socialNetwork);
    void UpdateSocialNetwork(SocialNetwork socialNetwork);
    
    
}