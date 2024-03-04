using EventPro.Domain.SharedContext.Entities;
using EventPro.Domain.SharedContext.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventPro.Domain.EventContext.Entities;

public sealed class Event : Entity
{
    public Event() { }

    public Event(string local, DateTime eventDate, string theme, int quantityPeople, string imageUrl, string email, IEnumerable<Lot> lots, IEnumerable<SocialNetwork> socialNetworks, IEnumerable<SpeakerEvent> speakerEvents)
    {
        ValidateDomain(local, eventDate, theme, quantityPeople, imageUrl, email, lots, socialNetworks, speakerEvents);
    }

    [JsonConstructor]
    public Event(int id, string local, DateTime eventDate, string theme, int quantityPeople, string imageUrl, string email, IEnumerable<Lot> lots, IEnumerable<SocialNetwork> socialNetworks, IEnumerable<SpeakerEvent> speakerEvents)
    {
        DomainValidation.When(id < 0, "Invalid Id value");
        Id = id;
        ValidateDomain(local, eventDate, theme, quantityPeople, imageUrl, email, lots, socialNetworks, speakerEvents);
    }

    public string Local  { get; private set; }
    public DateTime EventDate { get; private set; }
    public string Theme { get; private set; }
    public int QuantityPeople { get; private set; }
    public string ImageUrl { get; private set; }
    public string Email { get; private set; }
    public IEnumerable<Lot> Lots { get; private set; }
    public IEnumerable<SocialNetwork> SocialNetworks { get; private set; }
    public IEnumerable<SpeakerEvent> SpeakerEvents { get; private set; }

    private void ValidateDomain(string local, DateTime eventDate, string theme, int quantityPeople, string imageUrl, string email, IEnumerable<Lot> lots, IEnumerable<SocialNetwork> socialNetworks, IEnumerable<SpeakerEvent> speakerEvents)
    {
        DomainValidation.When(string.IsNullOrEmpty(local), "Invalid local. Local is required");
        DomainValidation.When(local.Length < 3, "Invalid local, to short, minimum 3 characters");  
        DomainValidation.When(string.IsNullOrEmpty(theme), "Invalid theme. Theme is required");
        DomainValidation.When(theme.Length < 3, "Invalid theme, to short, minimum 3 characters");  
        DomainValidation.When(quantityPeople < 0, "Invalid quantity people. Quantity people is required");
        DomainValidation.When(string.IsNullOrEmpty(imageUrl), "Invalid imageUrl. ImageUrl is required");
        DomainValidation.When(imageUrl.Length < 3, "Invalid imageUrl, to short, minimum 3 characters");
        DomainValidation.When(email.Length > 250, "Invalid email, to long, maximum 250 characters");
        DomainValidation.When(email.Length < 6, "Invalid email, to short, minimum 6 characters");

        Local = local;
        EventDate = eventDate;
        Theme = theme;
        QuantityPeople = quantityPeople;
        ImageUrl = imageUrl;
        Email = email;
        Lots = lots;
        SocialNetworks = socialNetworks;
        SpeakerEvents = speakerEvents;
    }

    public void Update(string local, DateTime eventDate, string theme, int quantityPeople, string imageUrl, string email, IEnumerable<Lot> lots, IEnumerable<SocialNetwork> socialNetworks, IEnumerable<SpeakerEvent> speakerEvents)
    {
        ValidateDomain(local, eventDate, theme, quantityPeople, imageUrl, email, lots, socialNetworks, speakerEvents);
    }
}