using System.Text.Json.Serialization;

namespace EventPro.Domain.EventContext.Entities;

public class SpeakerEvent
{
    public SpeakerEvent() { }

    [JsonConstructor]
    public SpeakerEvent(int speakerId, Speaker speaker, int eventId, Event @event)
    {
        ValidateDomain(speakerId, speaker, eventId, @event);
    }

    public int SpeakerId { get; private set; }
    public Speaker Speaker { get; private set; }
    public int EventId { get; private set; }
    public Event Event { get; private set; }

    private void ValidateDomain(int speakerId, Speaker speaker, int eventId, Event @event)
    {
        SpeakerId = speakerId;
        Speaker = speaker;
        EventId = eventId;
        Event = @event;
    }

    public void Update(int speakerId, Speaker speaker, int eventId, Event @event)
    {
        ValidateDomain(speakerId, speaker, eventId, @event);
    }
}