using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetEventByIdCommand : IRequest<EventDto>
{
    public int UserId { get; set; }
    public int EventId { get; set; }
}