using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class DeleteSocialNetworkCommand : IRequest<SocialNetwork>
{
    public int Id { get; set; }
}