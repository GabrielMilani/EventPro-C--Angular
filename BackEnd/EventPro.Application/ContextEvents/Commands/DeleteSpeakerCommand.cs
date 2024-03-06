using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class DeleteSpeakerCommand : IRequest<Speaker>
{
    public int Id { get; set; }
}