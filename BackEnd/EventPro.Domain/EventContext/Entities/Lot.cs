using EventPro.Domain.SharedContext.Entities;
using EventPro.Domain.SharedContext.Validation;
using System.Text.Json.Serialization;

namespace EventPro.Domain.EventContext.Entities;

public sealed class Lot : Entity
{
    public Lot() { }

    public Lot(string name, decimal price, DateTime initialDate, DateTime finalDate, int quantity, int eventId, Event @event)
    {
        ValidateDomain(name, price, initialDate, finalDate, quantity, eventId, @event);
    }

    [JsonConstructor]
    public Lot(int id, string name, decimal price, DateTime initialDate, DateTime finalDate, int quantity, int eventId, Event @event)
    {
        DomainValidation.When(id < 0, "Invalid Id value");
        Id = id;
        ValidateDomain(name, price, initialDate, finalDate, quantity, eventId, @event);
    }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime InitialDate { get; private set; }
    public DateTime FinalDate { get; private set; }
    public int Quantity { get; private set; }
    public int EventId { get; private set; }
    public Event Event { get; private set; }

    private void ValidateDomain(string name, decimal price, DateTime initialDate, DateTime finalDate, int quantity, int eventId, Event @event)
    {
        Name = name;
        Price = price;
        InitialDate = initialDate;
        FinalDate = finalDate;
        Quantity = quantity;
        EventId = eventId;
        Event = @event;
    }

    public void Update(string name, decimal price, DateTime initialDate, DateTime finalDate, int quantity, int eventId, Event @event)
    {
        ValidateDomain(name, price, initialDate, finalDate, quantity, eventId, @event);
    }
}