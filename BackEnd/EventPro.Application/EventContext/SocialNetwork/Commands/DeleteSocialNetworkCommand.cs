using EventPro.Application.EventContext.Lot.Commands;
using EventPro.Domain.SharedContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.SocialNetwork.Commands;

public class DeleteSocialNetworkCommand  : IRequest<Domain.EventContext.Entities.SocialNetwork>
{
    public int Id { get; set; }

    public class DeleteSocialNetworkCommandHandler : IRequestHandler<DeleteSocialNetworkCommand, Domain.EventContext.Entities.SocialNetwork>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSocialNetworkCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Domain.EventContext.Entities.SocialNetwork> Handle(DeleteSocialNetworkCommand request, CancellationToken cancellationToken)
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
}