using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ISpeakerRepository
{
    Task<IEnumerable<Speaker>> GetSpeakers();
    Task<Speaker> GetSpeakerById(int speakerId);
    Task<Speaker> AddSpeaker(Speaker speaker);
    void UpdateSpeaker(Speaker speaker);
    Task<Speaker> DeleteSpeaker(int speakerId); 
}