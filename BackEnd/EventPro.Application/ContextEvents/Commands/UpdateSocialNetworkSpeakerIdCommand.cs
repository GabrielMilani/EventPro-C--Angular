using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class UpdateSocialNetworkSpeakerIdCommand : IRequest<SocialNetwork>
{
    public int SpeakerId { get; set; }
    public SocialNetworkDto SocialNetworkDto { get; set; }
}