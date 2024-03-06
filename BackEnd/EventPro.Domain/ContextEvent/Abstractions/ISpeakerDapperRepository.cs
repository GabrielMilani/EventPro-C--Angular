using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ISpeakerDapperRepository
{
    Task<IEnumerable<Speaker>> GetSpeakers();
    Task<Speaker?> GetSpeakerById(int speakerId);
}