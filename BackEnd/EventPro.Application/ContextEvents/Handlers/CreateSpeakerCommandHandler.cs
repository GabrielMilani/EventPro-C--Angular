using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateSpeakerCommandHandler : IRequestHandler<CreateSpeakerCommand, Speaker>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSpeakerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Speaker> Handle(CreateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var newSpeaker = new Speaker(request.Name,
                                    request.Description, 
                                    request.ImageUrl,
                                    request.Telephone, 
                                    request.Email);
        await _unitOfWork.SpeakerRepository.AddSpeaker(newSpeaker);
        await _unitOfWork.CommitAsync();

        return newSpeaker;
    }
}