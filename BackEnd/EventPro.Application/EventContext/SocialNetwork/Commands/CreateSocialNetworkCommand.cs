using EventPro.Application.EventContext.Lot.Commands;
using EventPro.Domain.SharedContext.Abstractions;

namespace EventPro.Application.EventContext.SocialNetwork.Commands;

public class CreateSocialNetworkCommand  : SocialNetworkCommandBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSocialNetworkCommand(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    public async Task<Domain.EventContext.Entities.SocialNetwork> Handle(CreateSocialNetworkCommand request, CancellationToken cancellationToken)
    {
        var newSocialNetwork = new Domain.EventContext.Entities.SocialNetwork(request.Name,
                                                                              request.URL, 
                                                                              request.EventId,
                                                                              request.Event, 
                                                                              request.SpeakerId,
                                                                              request.Speaker);
        await _unitOfWork.SocialNetworkRepository.AddSocialNetwork(newSocialNetwork);
        await _unitOfWork.CommitAsync();

        return newSocialNetwork;
    } 
}