using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteSocialNetworkCommandHandler : IRequestHandler<DeleteSocialNetworkCommand, SocialNetwork>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSocialNetworkCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SocialNetwork> Handle(DeleteSocialNetworkCommand request, CancellationToken cancellationToken)
    {
        var deletedSocialNetwork = await _unitOfWork.SocialNetworkRepository.DeleteSocialNetwork(request.Id);

        if (deletedSocialNetwork is null)
        {
            throw new InvalidOperationException("SocialNetwork not found");
        }

        await _unitOfWork.CommitAsync();
        return deletedSocialNetwork;
    }
}