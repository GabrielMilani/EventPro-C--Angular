using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextEvent.Entities.Identity;

namespace EventPro.Application.DTOs;

public class EventDto
{
    public int? Id { get; set; }
    public string? Theme { get; set; }
    public string? Local  { get; set; }
    public string? Email { get; set; }
    public string? ImageUrl { get; set; }
    public string? Telephone { get; set; }
    public int? QuantityPeople { get; set; }
    public DateTime? EventDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Lot> Lots{ get;  set; }
    public List<SocialNetwork> SocialNetworks { get;  set; }
    public List<SpeakerEvent> SpeakerEvents { get;  set; }
}