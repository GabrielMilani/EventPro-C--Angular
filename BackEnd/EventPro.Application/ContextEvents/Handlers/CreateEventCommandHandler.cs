using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Event>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Event(request.Theme,
                                 request.Local,
                                 request.Email,
                                 request.ImageUrl,
                                 request.Telephone,
                                 request.QuantityPeople, 
                                 request.EventDate);

        await _unitOfWork.EventRepository.AddEvent(newEvent);
        await _unitOfWork.CommitAsync();

        return newEvent;
    }
}