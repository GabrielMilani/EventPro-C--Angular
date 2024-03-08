using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class CreateLotCommand : IRequest<Lot>
{
    public int EventId { get; set; }
    public LotDto Lot { get; set; }
}