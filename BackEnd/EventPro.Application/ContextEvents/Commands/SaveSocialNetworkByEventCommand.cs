using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class SaveSocialNetworkByEventCommand : IRequest<SocialNetworkDto[]>
{
    public int EventId { get; set; }
    public SocialNetworkDto[] SocialNetworkDto { get; set; }
}