using EventPro.Domain.SharedContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.Event.Commands;

public class UpdateEventCommand : EventCommandBase
{
    public int Id { get; set; }  

    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Domain.EventContext.Entities.Event>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Domain.EventContext.Entities.Event> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _unitOfWork.EventRepository.GetEventById(request.Id);

            if (existingEvent == null)
            {
                throw new InvalidOperationException("Event not found");
            }
            existingEvent.Update(request.Local,
                                 request.EventDate, 
                                 request.Theme,
                                 request.QuantityPeople, 
                                 request.ImageUrl,
                                 request.Email,
                                 request.Lots,
                                 request.SocialNetworks,
                                 request.SpeakerEvents);
            _unitOfWork.EventRepository.UpdateEvent(existingEvent);
            await _unitOfWork.CommitAsync();

            return existingEvent;
        }
    }
}