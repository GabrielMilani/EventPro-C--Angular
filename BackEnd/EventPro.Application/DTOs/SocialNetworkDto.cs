using AutoMapper;
using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Application.DTOs;

public class SocialNetworkDto
{
    public int? Id { get; set; }
    public string? Name { get;  set; }
    public string? URL { get;  set; }
}