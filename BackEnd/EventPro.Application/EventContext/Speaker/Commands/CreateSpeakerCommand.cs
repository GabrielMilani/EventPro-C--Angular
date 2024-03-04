using EventPro.Application.EventContext.Lot.Commands;
using EventPro.Domain.SharedContext.Abstractions;

namespace EventPro.Application.EventContext.Speaker.Commands;

public class CreateSpeakerCommand : SpeakerCommandBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSpeakerCommand(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    public async Task<Domain.EventContext.Entities.Speaker> Handle(CreateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var newSpeaker = new Domain.EventContext.Entities.Speaker(request.Name,
                                                                  request.MiniCV, 
                                                                  request.ImageUrl,
                                                                  request.Telephone, 
                                                                  request.Email,
                                                                  request.SocialNetworks,
                                                                  request.SpeakerEvents);
        await _unitOfWork.SpeakerRepository.AddSpeaker(newSpeaker);
        await _unitOfWork.CommitAsync();

        return newSpeaker;
    } 
}