using EventPro.Domain.EventContext.Entities;
using MediatR;

namespace EventPro.Application.EventContext.Speaker.Commands;

public class SpeakerCommandBase : IRequest<Domain.EventContext.Entities.Speaker>
{
    public string Name { get; set; }
    public string MiniCV { get; set; }
    public string ImageUrl { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public IEnumerable<Domain.EventContext.Entities.SocialNetwork> SocialNetworks { get; set; }
    public IEnumerable<Domain.EventContext.Entities.SpeakerEvent> SpeakerEvents { get; set; }    
}