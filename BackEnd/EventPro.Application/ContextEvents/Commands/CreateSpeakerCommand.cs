using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class CreateSpeakerCommand : IRequest<SpeakerDto>
{
    public int UserId { get; set; }
    public SpeakerAddDto SpeakerAddDto { get; set; }
}