using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Models;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetSpeakersCommand : IRequest<PageList<SpeakerDto>>
{
    public PageParams PageParams { get; set; }
    public bool IncludeEvents { get; set; }
}