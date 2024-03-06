using EventPro.Domain.ContextShared.Entities;
using System.Text.Json.Serialization;

namespace EventPro.Domain.ContextEvent.Entities;

public sealed class SocialNetwork: Entity
{
    public SocialNetwork() { }

    public SocialNetwork(string? name, string? url, int? eventId, Event? @event, int? speakerId, Speaker? speaker)
    {
        ValidateDomain(name, url, eventId, @event, speakerId, speaker);
    }
    [JsonConstructor]
    public SocialNetwork(int id, string? name, string? url, int? eventId, Event? @event, int? speakerId, Speaker? speaker)
    {
        Id = id;
        ValidateDomain(name, url, eventId, @event, speakerId, speaker);
    }
    public string? Name { get; private set; }
    public string? URL { get; private set; }
    public int? EventId { get; private set; }
    public Event? Event { get; private set; }
    public int? SpeakerId { get; private set; }
    public Speaker? Speaker { get; private set; } 

    public void Update(string? name, string? url, int? eventId, Event? @event, int? speakerId, Speaker? speaker)
    {
        ValidateDomain(name, url, eventId, @event, speakerId, speaker);
    }

    private void ValidateDomain(string? name, string? url, int? eventId, Event? @event, int? speakerId, Speaker? speaker)
    {
        Name = name;
        URL = url;
        EventId = eventId;
        Event = @event;
        SpeakerId = speakerId;
        Speaker = speaker;
    }
}