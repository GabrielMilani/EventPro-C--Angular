using EventPro.Domain.SharedContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.Event.Commands;

public class DeleteEventCommand : IRequest<Domain.EventContext.Entities.Event>
{
    public int Id { get; set; }

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Domain.EventContext.Entities.Event>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Domain.EventContext.Entities.Event> Handle(DeleteEventCommand request,
            CancellationToken cancellationToken)
        {
            var deletedEvent = await _unitOfWork.EventRepository.DeleteEvent(request.Id);

            if (deletedEvent is null)
            {
                throw new InvalidOperationException("Event not found");
            }

            await _unitOfWork.CommitAsync();
            return deletedEvent;
        }
    }
}
