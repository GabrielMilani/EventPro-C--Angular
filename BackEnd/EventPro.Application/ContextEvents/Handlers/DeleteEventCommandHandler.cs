using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Event>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Event> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        
        var deletedEvent = await _unitOfWork.EventRepository.DeleteEvent(request.UserId, request.Id);

        if (deletedEvent is null)
        {
            throw new InvalidOperationException("Event not found");
        }

        await _unitOfWork.CommitAsync();
        return deletedEvent;
    }
}