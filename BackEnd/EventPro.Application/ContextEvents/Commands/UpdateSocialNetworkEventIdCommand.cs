using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class UpdateSocialNetworkEventIdCommand : IRequest<SocialNetwork>
{
    public int EventId { get; set; }
    public SocialNetworkDto SocialNetworkDto { get; set; }
}