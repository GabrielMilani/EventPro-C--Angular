using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class CreateSocialNetworkCommand : IRequest<SocialNetworkDto>
{
    public string Name { get; set; }
    public string URL { get; set; }
    public int? EventId { get; set; }
    public Event Event { get; set; }
    public int? SpeakerId { get; set; }
    public Speaker Speaker { get;  set; }    
}