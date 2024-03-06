﻿using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class UpdateEventCommand : IRequest<Event>
{
    public int Id { get; set; }
    public string Local  { get;  set; }
    public DateTime EventDate { get;  set; }
    public string Theme { get;  set; }
    public int QuantityPeople { get;  set; }
    public string ImageUrl { get;  set; }
    public string Email { get;  set; } 
    public string Telephone { get; set; }
    public IEnumerable<Lot> Lots{ get; set; }
    public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
    public IEnumerable<SpeakerEvent> SpeakerEvents { get; set; }

}