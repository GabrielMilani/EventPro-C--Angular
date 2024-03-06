using System.Text.Json.Serialization;
using EventPro.Domain.ContextShared.Entities;

namespace EventPro.Domain.ContextEvent.Entities;

public sealed class Lot: Entity
{
    public Lot() { }

    public Lot(string? name, int? quantity, decimal? price, DateTime? initialDate, DateTime? finalDate, 
        int? eventId, Event? @event)
    {
        ValidateDomain(name, quantity, price, initialDate, finalDate, eventId, @event);
    }
    [JsonConstructor]
    public Lot(int id, string? name, int? quantity, decimal? price, DateTime? initialDate, DateTime? finalDate, 
        int? eventId, Event? @event)
    {
        Id = id;
        ValidateDomain(name, quantity, price, initialDate, finalDate, eventId, @event);
    }

    public string? Name { get; private set; }
    public int? Quantity { get; private set; }
    public decimal? Price { get; private set; }
    public DateTime? InitialDate { get; private set; }
    public DateTime? FinalDate { get; private set; }
    public int? EventId { get; private set; }
    public Event? Event { get; private set; }

    public void Update(string? name, int? quantity, decimal? price, DateTime? initialDate, DateTime? finalDate, 
        int? eventId, Event? @event)
    {
        ValidateDomain(name, quantity, price, initialDate, finalDate, eventId, @event);
    }

    private void ValidateDomain(string? name, int? quantity, decimal? price, DateTime? initialDate, DateTime? finalDate, 
        int? eventId, Event? @event)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        InitialDate = initialDate;
        FinalDate = finalDate;
        EventId = eventId;
        Event = @event;
    }
}