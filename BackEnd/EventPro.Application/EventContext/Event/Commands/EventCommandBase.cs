using EventPro.Domain.EventContext.Entities;
using MediatR;

namespace EventPro.Application.EventContext.Event.Commands;

public class EventCommandBase : IRequest<Domain.EventContext.Entities.Event>
{
    public string Local  { get;  set; }
    public DateTime EventDate { get;  set; }
    public string Theme { get;  set; }
    public int QuantityPeople { get;  set; }
    public string ImageUrl { get;  set; }
    public string Email { get;  set; }
    public IEnumerable<Domain.EventContext.Entities.Lot> Lots { get;  set; }
    public IEnumerable<Domain.EventContext.Entities.SocialNetwork> SocialNetworks { get;  set; }
    public IEnumerable<Domain.EventContext.Entities.SpeakerEvent> SpeakerEvents { get;  set; }
}                                                       