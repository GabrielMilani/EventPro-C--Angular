using MediatR;

namespace EventPro.Application.EventContext.Lot.Commands;

public class LotCommandBase : IRequest<EventPro.Domain.EventContext.Entities.Lot>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public int Quantity { get; set; }
    public int EventId { get; set; }
    public EventPro.Domain.EventContext.Entities.Event Event { get; set; }
}