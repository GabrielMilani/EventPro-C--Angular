using System.Text.Json.Serialization;
using EventPro.Domain.ContextShared.Entities;

namespace EventPro.Domain.ContextEvent.Entities;

public sealed class Event: Entity
{
    public Event() { }

    public Event(string? theme, string? local, string? email, string? imageUrl, string? telephone, int? quantityPeople, DateTime? eventDate)
    {
        ValidateDomain(theme, local, email, imageUrl, telephone, quantityPeople, eventDate);
    }

    [JsonConstructor]
    public Event(int id, string? theme, string? local, string? email, string? imageUrl, string? telephone, int? quantityPeople, DateTime? eventDate)
    {
        Id = id;
        Lots = new List<Lot>();
        SocialNetworks = new List<SocialNetwork>();
        SpeakerEvents = new List<SpeakerEvent>();
        ValidateDomain(theme, local, email, imageUrl, telephone, quantityPeople, eventDate);
    }

    public string? Theme { get; private set; }
    public string? Local  { get; private set; }
    public string? Email { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? Telephone { get; private set; }
    public int? QuantityPeople { get; private set; }
    public DateTime? EventDate { get; private set; }
    public List<Lot> Lots{ get; private set; }
    public List<SocialNetwork> SocialNetworks { get; private set; }
    public List<SpeakerEvent> SpeakerEvents { get; private set; }

    public void Update(string? theme, string? local, string? email, string? imageUrl, string? telephone,
        int? quantityPeople, DateTime? eventDate)
    {
        ValidateDomain(theme, local, email, imageUrl, telephone, quantityPeople, eventDate);
    }
    
    private void ValidateDomain(string? theme, string? local, string? email, string? imageUrl, string? telephone, 
        int? quantityPeople, DateTime? eventDate)
    {
        Theme = theme;
        Local = local;
        Email = email;
        ImageUrl = imageUrl;
        Telephone = telephone;
        QuantityPeople = quantityPeople;
        EventDate = eventDate;
    }

}