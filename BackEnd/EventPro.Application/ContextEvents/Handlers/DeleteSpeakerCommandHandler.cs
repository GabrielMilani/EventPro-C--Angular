using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteSpeakerCommandHandler : IRequestHandler<DeleteSpeakerCommand, Speaker>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSpeakerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Speaker> Handle(DeleteSpeakerCommand request, CancellationToken cancellationToken)
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