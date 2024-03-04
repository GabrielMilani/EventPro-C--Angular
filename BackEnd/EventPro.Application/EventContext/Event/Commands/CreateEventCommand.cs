using EventPro.Domain.SharedContext.Abstractions;

namespace EventPro.Application.EventContext.Event.Commands;

public class CreateEventCommand : EventCommandBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateEventCommand(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    public async Task<Domain.EventContext.Entities.Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Domain.EventContext.Entities.Event(request.Local,
                                                              request.EventDate, 
                                                              request.Theme,
                                                              request.QuantityPeople, 
                                                              request.ImageUrl,
                                                              request.Email,
                                                              request.Lots,
                                                              request.SocialNetworks,
                                                              request.SpeakerEvents);
        await _unitOfWork.EventRepository.AddEvent(newEvent);
        await _unitOfWork.CommitAsync();

        return newEvent;
    } 
}