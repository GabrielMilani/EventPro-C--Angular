using AutoMapper;
using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Application.DTOs;

public class SocialNetworkDto
{
    public int? Id { get; set; }
    public string? Name { get; private set; }
    public string? URL { get; private set; }
    public int? EventId { get; private set; }
    public Event? Event { get; private set; }
    public int? SpeakerId { get; private set; }
    public Speaker? Speaker { get; private set; } 
}