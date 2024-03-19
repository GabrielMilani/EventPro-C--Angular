using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextEvent.Entities.Identity;

namespace EventPro.Application.DTOs;

public class SpeakerAddDto
{
    public int? Id { get; set; }
    public string MiniCV { get; set; }
    public int UserId { get; set; }
}