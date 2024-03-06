using System.Text.Json.Serialization;
using EventPro.Domain.ContextShared.Entities;

namespace EventPro.Domain.ContextEvent.Entities;

public sealed class Speaker : Entity
{
    public Speaker()
    {
    }

    public Speaker(string? name, string? description, string? imageUrl, string? telephone, string? email)
    {
        ValidationDomain(name, description, imageUrl, telephone, email);
    }

    [JsonConstructor]
    public Speaker(int id, string? name, string? description, string? imageUrl, string? telephone, string? email)
    {
        Id = id;
        SocialNetworks = new List<SocialNetwork>();
        SpeakerEvents = new List<SpeakerEvent>(); 
        ValidationDomain(name, description, imageUrl, telephone, email);
    }

    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? Telephone { get; private set; }
    public string? Email { get; private set; }
    public List<SocialNetwork> SocialNetworks { get; private set; }
    public List<SpeakerEvent> SpeakerEvents { get; private set; }
    
    public void Update(string? name, string? description, string? imageUrl, string? telephone, string? email)
    {
        ValidationDomain(name, description, imageUrl, telephone, email);
    }

    private void ValidationDomain(string? name, string? description, string? imageUrl, string? telephone, string? email)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Telephone = telephone;
        Email = email;
    }
}