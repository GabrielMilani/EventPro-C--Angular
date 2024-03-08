using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class CreateEventCommand : IRequest<EventDto>
{
    public string Local  { get;  set; }
    public DateTime EventDate { get;  set; }
    public string Theme { get;  set; }
    public int QuantityPeople { get;  set; }
    public string Email { get;  set; }
    public string ImageUrl { get;  set; }
    public string Telephone { get; set; }
}