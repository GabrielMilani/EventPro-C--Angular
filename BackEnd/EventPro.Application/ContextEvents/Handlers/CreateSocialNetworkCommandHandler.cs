using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateSocialNetworkCommandHandler : IRequestHandler<CreateSocialNetworkCommand, SocialNetworkDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateSocialNetworkCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SocialNetworkDto> Handle(CreateSocialNetworkCommand request, CancellationToken cancellationToken)
    {
        var newSocialNetwork = _mapper.Map<SocialNetwork>(request);
        await _unitOfWork.SocialNetworkRepository.AddSocialNetwork(newSocialNetwork);
        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<SocialNetworkDto>(newSocialNetwork);
        return result;
    }
}