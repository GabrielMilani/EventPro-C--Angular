using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class UpdateSpeakerCommand : IRequest<SpeakerDto>
{
    public int UserId { get; set; }
    public SpeakerUpdateDto SpeakerUpdateDto { get; set; }
}