using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetLotByIdsCommand : IRequest<Lot>
{
    public int EventId { get; set; }
    public int Id { get; set; }
}