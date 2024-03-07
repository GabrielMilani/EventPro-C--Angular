using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetLotsByEventIdCommand : IRequest<List<Lot>>
{
    public int EventId { get; set; }
}