using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetEventsCommand : IRequest<EventDto[]>
{
    public int UserId { get; set; }
}