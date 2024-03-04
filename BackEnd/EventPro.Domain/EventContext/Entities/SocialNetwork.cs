using System.Text.Json.Serialization;
using EventPro.Domain.SharedContext.Entities;
using EventPro.Domain.SharedContext.Validation;

namespace EventPro.Domain.EventContext.Entities;

public class SocialNetwork : Entity
{
    public SocialNetwork() { }

    public SocialNetwork(string name, string url, int? eventId, Event @event, int? speakerId, Speaker speaker)
    {
        ValidateDomain(name, url, eventId, @event, speakerId, speaker);
    }

    [JsonConstructor]
    public SocialNetwork(int id, string name, string url, int? eventId, Event @event, int? speakerId, Speaker speaker)
    {           
        DomainValidation.When(id < 0, "Invalid Id value");
        Id = id;
        ValidateDomain(name, url, eventId, @event, speakerId, speaker);
    }

    public string Name { get; private set; }
    public string URL { get; private set; }
    public int? EventId { get; private set; }
    public Event Event { get; private set; }
    public int? SpeakerId { get; private set; }
    public Speaker Speaker { get; private set; }

    private void ValidateDomain(string name, string url, int? eventId, Event @event, int? speakerId, Speaker speaker)
    {
        Name = name;
        URL = url;
        EventId = eventId;
        Event = @event;
        SpeakerId = speakerId;
        Speaker = speaker;
    }
    public void Update(string name, string url, int? eventId, Event @event, int? speakerId, Speaker speaker)
    {
        ValidateDomain(name, url, eventId, @event, speakerId, speaker);
    }
}