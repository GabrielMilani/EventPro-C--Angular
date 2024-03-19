using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class DeleteSocialNetworkEventCommand : IRequest<bool>
{
    public int EventId { get; set; }
    public int SocialNetworkId { get; set; }

}