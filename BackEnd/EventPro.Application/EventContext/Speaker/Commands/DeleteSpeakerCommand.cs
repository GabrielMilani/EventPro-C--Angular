using EventPro.Application.EventContext.Lot.Commands;
using EventPro.Domain.SharedContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.Speaker.Commands;

public class DeleteSpeakerCommand : IRequest<Domain.EventContext.Entities.Speaker>
{
    public int Id { get; set; }

    public class DeleteSpeakerCommandHandler : IRequestHandler<DeleteSpeakerCommand, Domain.EventContext.Entities.Speaker>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSpeakerCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Domain.EventContext.Entities.Speaker> Handle(DeleteSpeakerCommand request, CancellationToken cancellationToken)
        {
            var deletedSpeaker = await _unitOfWork.SpeakerRepository.DeleteSpeaker(request.Id);

            if (deletedSpeaker is null)
            {
                throw new InvalidOperationException("Speaker not found");
            }

            await _unitOfWork.CommitAsync();
            return deletedSpeaker;
        }
    }
}