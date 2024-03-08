using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class DeleteEventCommand : IRequest<Event>
{
    public int Id { get; set; } 
}