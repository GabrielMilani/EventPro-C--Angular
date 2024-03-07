using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class SaveLotsCommand : IRequest<List<Lot>>
{
    public int EventId { get; set; }
    public List<Lot> Lots { get; set; }
}