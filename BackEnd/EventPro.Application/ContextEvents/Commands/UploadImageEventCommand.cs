using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class UploadImageEventCommand : IRequest<EventDto>
{
    public int Id { get; set; }
    public EventDto EventDto { get; set; }
}