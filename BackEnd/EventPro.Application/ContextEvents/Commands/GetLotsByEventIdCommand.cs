using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetLotsByEventIdCommand : IRequest<LotDto[]>
{
    public int EventId { get; set; }
}