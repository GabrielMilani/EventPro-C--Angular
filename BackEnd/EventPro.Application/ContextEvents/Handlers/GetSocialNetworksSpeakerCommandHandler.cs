using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetSocialNetworksSpeakerCommandHandler : IRequestHandler<GetSocialNetworksSpeakerCommand, SocialNetworkDto[]>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSocialNetworksSpeakerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SocialNetworkDto[]> Handle(GetSocialNetworksSpeakerCommand request, CancellationToken cancellationToken)
    {
        var socialNetwork = await _unitOfWork.SocialNetworkRepository.GetSocialNetworksBySpeakerId(request.SpeakerId);
        if (socialNetwork == null) return null;

        var result = _mapper.Map<SocialNetworkDto[]>(socialNetwork);
        return result;
    }
}