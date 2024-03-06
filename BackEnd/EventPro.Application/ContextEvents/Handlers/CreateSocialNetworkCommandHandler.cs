using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateSocialNetworkCommandHandler : IRequestHandler<CreateSocialNetworkCommand, SocialNetwork>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSocialNetworkCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SocialNetwork> Handle(CreateSocialNetworkCommand request, CancellationToken cancellationToken)
    {
        var newSocialNetwork = new SocialNetwork(request.Name,
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