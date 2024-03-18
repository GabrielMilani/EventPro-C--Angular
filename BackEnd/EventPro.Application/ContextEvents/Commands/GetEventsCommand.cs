using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Models;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetEventsCommand : IRequest<PageList<EventDto>>
{
    public int UserId { get; set; }
    public PageParams PageParams { get; set; }
    public bool IncludeSpeaker { get; set; } = false;

}