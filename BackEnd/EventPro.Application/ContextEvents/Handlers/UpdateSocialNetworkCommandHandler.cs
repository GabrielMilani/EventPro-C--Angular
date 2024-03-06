using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateSocialNetworkCommandHandler : IRequestHandler<UpdateSocialNetworkCommand, SocialNetwork>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSocialNetworkCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SocialNetwork> Handle(UpdateSocialNetworkCommand request, CancellationToken cancellationToken)
    {
        var existingSocialNetwork = await _unitOfWork.SocialNetworkRepository.GetSocialNetworkById(request.Id);

        if (existingSocialNetwork == null)
        {
            throw new InvalidOperationException("Member not found");
        }
        existingSocialNetwork.Update(request.Name, 
                                    request.URL, 
                                    request.EventId, 
                                    request.Event,
                                    request.SpeakerId,
                                    request.Speaker);
        _unitOfWork.SocialNetworkRepository.UpdateSocialNetwork(existingSocialNetwork);
        await _unitOfWork.CommitAsync();

        return existingSocialNetwork;
    }
}