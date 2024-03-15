using System.Text.Json.Serialization;
using EventPro.Domain.ContextShared.Entities;

namespace EventPro.Domain.ContextEvent.Entities;

public sealed class Lot: Entity
{
    public string? Name { get;  set; }
    public int? Quantity { get;  set; }
    public decimal? Price { get;  set; }
    public DateTime? InitialDate { get;  set; }
    public DateTime? FinalDate { get;  set; }
    public int? EventId { get;  set; }
    public Event Event { get;  set; }
}