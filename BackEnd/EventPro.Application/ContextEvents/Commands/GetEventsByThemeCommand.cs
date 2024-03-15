using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetEventsByThemeCommand : IRequest<EventDto[]>
{
    public int UserId { get; set; }
    public string Theme { get; set; }
}