using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Models;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetSpeakerByUserIdCommand : IRequest<SpeakerDto>
{
    public int UserId { get; set; }
    public bool IncludeEvents { get; set; }
}