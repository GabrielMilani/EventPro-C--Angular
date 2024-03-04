using EventPro.Domain.EventContext.Entities;

namespace EventPro.Domain.EventContext.Abstractions;

public interface ISpeakerRepository
{
    Task<IEnumerable<Speaker>> GetSpeakerAll();
    Task<Speaker> GetSpeakerById(int speakerId);
    Task<Speaker> AddSpeaker(Speaker speaker);
    void UpdateSpeaker(Speaker speaker);
    Task<Speaker> DeleteSpeaker(int speakerId);
}