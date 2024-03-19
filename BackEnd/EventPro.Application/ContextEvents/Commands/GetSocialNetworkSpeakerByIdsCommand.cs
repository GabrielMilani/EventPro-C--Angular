using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetSocialNetworkSpeakerByIdsCommand : IRequest<SocialNetworkDto>
{
    public int SpeakerId { get; set; }
    public int SocialNetworkId { get; set; }
}