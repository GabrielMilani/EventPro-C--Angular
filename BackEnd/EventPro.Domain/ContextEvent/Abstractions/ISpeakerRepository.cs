using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Models;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ISpeakerRepository
{
    Task<PageList<Speaker>> GetSpeakers(PageParams pageParams, bool includeEvents = false);
    Task<Speaker> GetSpeakerByUserId(int userId, bool includeEvents = false);
    
    Task<Speaker> AddSpeaker(Speaker speaker);
    void UpdateSpeaker(Speaker speaker);
}