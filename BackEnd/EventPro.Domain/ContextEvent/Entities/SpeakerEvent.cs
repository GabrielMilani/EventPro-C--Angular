namespace EventPro.Domain.ContextEvent.Entities;

public sealed class SpeakerEvent
{
    public int? SpeakerId { get; private set; }
    public Speaker? Speaker { get; private set; }
    public int? EventId { get; private set; }
    public Event? Event { get; private set; }
}