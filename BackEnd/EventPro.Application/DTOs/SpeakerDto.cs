using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Application.DTOs;

public class SpeakerDto
{
    public int? Id { get; set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? Telephone { get; private set; }
    public string? Email { get; private set; }
}