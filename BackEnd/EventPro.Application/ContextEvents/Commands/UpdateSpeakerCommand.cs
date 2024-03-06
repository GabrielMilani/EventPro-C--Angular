﻿using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class UpdateSpeakerCommand : IRequest<Speaker>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
    public IEnumerable<SpeakerEvent> SpeakerEvents { get; set; } 
}