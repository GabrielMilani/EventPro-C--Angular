using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetSocialNetworksSpeakerCommand : IRequest<SocialNetworkDto[]>
{
    public int SpeakerId { get; set; }
}