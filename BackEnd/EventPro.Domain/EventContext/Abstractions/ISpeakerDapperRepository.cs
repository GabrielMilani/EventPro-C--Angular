using EventPro.Domain.EventContext.Entities;

namespace EventPro.Domain.EventContext.Abstractions;

public interface ISpeakerDapperRepository
{
    Task<IEnumerable<Speaker>> GetSpeakers();
    Task<Speaker?> GetSpeakerById(int speakerId);    
}