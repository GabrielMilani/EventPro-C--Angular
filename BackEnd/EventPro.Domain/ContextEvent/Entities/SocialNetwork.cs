using EventPro.Domain.ContextShared.Entities;
using System.Text.Json.Serialization;

namespace EventPro.Domain.ContextEvent.Entities;

public sealed class SocialNetwork: Entity
{
    public string? Name { get;  set; }
    public string? URL { get;  set; }
    public int? EventId { get;  set; }
    public Event Event { get;  set; }
    public int? SpeakerId { get;  set; }
    public Speaker Speaker { get;  set; } 

}