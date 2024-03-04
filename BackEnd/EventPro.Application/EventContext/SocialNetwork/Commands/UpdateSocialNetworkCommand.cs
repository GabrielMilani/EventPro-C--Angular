using EventPro.Domain.SharedContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.SocialNetwork.Commands;

public class UpdateSocialNetworkCommand : SocialNetworkCommandBase
{
    public int Id { get; set; }

    public class UpdateSocialNetworkCommandHandler : IRequestHandler<UpdateSocialNetworkCommand, Domain.EventContext.Entities.SocialNetwork>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSocialNetworkCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Domain.EventContext.Entities.SocialNetwork> Handle(UpdateSocialNetworkCommand request, CancellationToken cancellationToken)
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
}