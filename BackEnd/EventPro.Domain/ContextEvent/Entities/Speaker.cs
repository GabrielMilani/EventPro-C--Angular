using System.Text.Json.Serialization;
using EventPro.Domain.ContextEvent.Entities.Identity;
using EventPro.Domain.ContextShared.Entities;

namespace EventPro.Domain.ContextEvent.Entities;

public sealed class Speaker : Entity
{
    public string? MiniCV { get; private set; }
    public int? UserId { get; set; }
    public User User { get; set; }
    public List<SocialNetwork> SocialNetworks { get; private set; }
    public List<SpeakerEvent> SpeakerEvents { get; private set; }
}