using MediatR;

namespace EventPro.Application.EventContext.SocialNetwork.Commands;

public class SocialNetworkCommandBase : IRequest<Domain.EventContext.Entities.SocialNetwork>
{
    public string Name { get; set; }
    public string URL { get; set; }
    public int? EventId { get; set; }
    public Domain.EventContext.Entities.Event Event { get; set; }
    public int? SpeakerId { get; set; }
    public Domain.EventContext.Entities.Speaker Speaker { get;  set; }    
}