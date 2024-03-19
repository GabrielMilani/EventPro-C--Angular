using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetSocialNetworkEventByIdsCommand : IRequest<SocialNetworkDto>
{
    public int EventId { get; set; }
    public int SocialNetworkId { get; set; }
}