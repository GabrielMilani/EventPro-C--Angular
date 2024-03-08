using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class UpdateSpeakerCommand : IRequest<SpeakerDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
}