using EventPro.Domain.SharedContext.Entities;
using EventPro.Domain.SharedContext.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventPro.Domain.EventContext.Entities;

public sealed class Speaker : Entity
{
    public Speaker() { }

    public Speaker(string name, string miniCv, string imageUrl, string telephone, string email, IEnumerable<SocialNetwork> socialNetworks, IEnumerable<SpeakerEvent> speakerEvents)
    {
        ValidateDomain(name, miniCv, imageUrl, telephone, email, socialNetworks, speakerEvents);
    }

    [JsonConstructor]
    public Speaker(int id, string name, string miniCv, string imageUrl, string telephone, string email, IEnumerable<SocialNetwork> socialNetworks, IEnumerable<SpeakerEvent> speakerEvents)
    {
        DomainValidation.When(id < 0, "Invalid Id value");
        Id = id;
        ValidateDomain(name, miniCv, imageUrl, telephone, email, socialNetworks, speakerEvents);
    }

    public string Name { get; private set; }
    public string MiniCV { get; private set; }
    public string ImageUrl { get; private set; }
    public string Telephone { get; private set; }
    public string Email { get; private set; }
    public IEnumerable<SocialNetwork> SocialNetworks { get; private set; }
    public IEnumerable<SpeakerEvent> SpeakerEvents { get; private set; }

    private void ValidateDomain(string name, string miniCv, string imageUrl, string telephone, string email, IEnumerable<SocialNetwork> socialNetworks, IEnumerable<SpeakerEvent> speakerEvents)
    {
        Name = name;
        MiniCV = miniCv;
        ImageUrl = imageUrl;
        Telephone = telephone;
        Email = email;
        SocialNetworks = socialNetworks;
        SpeakerEvents = speakerEvents;
    }
    public void Update(string name, string miniCv, string imageUrl, string telephone, string email, IEnumerable<SocialNetwork> socialNetworks, IEnumerable<SpeakerEvent> speakerEvents)
    {
        ValidateDomain(name, miniCv, imageUrl, telephone, email, socialNetworks, speakerEvents);
    }
}