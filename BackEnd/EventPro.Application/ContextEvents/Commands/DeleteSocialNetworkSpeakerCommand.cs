using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class DeleteSocialNetworkSpeakerCommand : IRequest<bool>
{
    public int SpeakerId { get; set; }
    public int SocialNetworkId { get; set; }

}