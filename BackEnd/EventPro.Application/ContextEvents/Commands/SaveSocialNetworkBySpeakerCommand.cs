using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class SaveSocialNetworkBySpeakerCommand : IRequest<SocialNetworkDto[]>
{
    public int SpeakerId { get; set; }
    public SocialNetworkDto[] SocialNetworkDto { get; set; }
}