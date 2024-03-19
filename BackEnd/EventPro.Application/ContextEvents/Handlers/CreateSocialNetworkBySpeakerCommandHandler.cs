using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateSocialNetworkBySpeakerCommandHandler : IRequestHandler<CreateSocialNetworkBySpeakerCommand, SocialNetwork>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSocialNetworkBySpeakerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SocialNetwork> Handle(CreateSocialNetworkBySpeakerCommand request, CancellationToken cancellationToken)
    {
        var newSocialNetwork = _mapper.Map<SocialNetwork>(request.SocialNetworkDto);
        newSocialNetwork.SpeakerId = request.SpeakerId;
        await _unitOfWork.SocialNetworkRepository.AddSocialNetwork(newSocialNetwork);
        await _unitOfWork.CommitAsync();
        return  newSocialNetwork;
    }
}