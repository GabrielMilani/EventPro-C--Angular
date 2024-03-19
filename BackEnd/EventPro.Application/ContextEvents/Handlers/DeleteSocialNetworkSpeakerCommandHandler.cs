using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteSocialNetworkSpeakerCommandHandler : IRequestHandler<DeleteSocialNetworkSpeakerCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteSocialNetworkSpeakerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteSocialNetworkSpeakerCommand request, CancellationToken cancellationToken)
    {
        var socialNetwork = await _unitOfWork.SocialNetworkRepository.DeleteSocialNetworkSpeakerId(request.SpeakerId, request.SocialNetworkId);
        if (socialNetwork == null) return false;
        await _unitOfWork.CommitAsync();
        return true;
    }
}