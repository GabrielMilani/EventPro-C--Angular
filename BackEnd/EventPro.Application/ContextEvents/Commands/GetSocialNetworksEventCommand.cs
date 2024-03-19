using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetSocialNetworksEventCommand : IRequest<SocialNetworkDto[]>
{
    public int EventId { get; set; }
}