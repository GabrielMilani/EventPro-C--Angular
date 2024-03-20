using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextEvent.Entities.Identity;

namespace EventPro.Application.DTOs;

public class SpeakerAddDto
{
    public int? Id { get; set; } = 0;
    public string MiniCV { get; set; }  = string.Empty;
    public int? UserId { get; set; } = 0;
}