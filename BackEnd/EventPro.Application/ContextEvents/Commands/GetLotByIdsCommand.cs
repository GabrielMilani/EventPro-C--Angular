using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetLotByIdsCommand : IRequest<LotDto>
{
    public int EventId { get; set; }
    public int Id { get; set; }
}