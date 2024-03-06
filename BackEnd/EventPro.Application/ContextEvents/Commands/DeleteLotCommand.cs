using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class DeleteLotCommand : IRequest<Lot>
{
    public int Id { get; set; } 
}