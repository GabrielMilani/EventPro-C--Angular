using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteSocialNetworkCommandHandler : IRequestHandler<DeleteSocialNetworkCommand, SocialNetworkDto>
{
    private readonly IUnitOfWork _unitOfWork; 
    private readonly IMapper _mapper;

    public DeleteSocialNetworkCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SocialNetworkDto> Handle(DeleteSocialNetworkCommand request, CancellationToken cancellationToken)
    {
        var deletedSocialNetwork = await _unitOfWork.SocialNetworkRepository.DeleteSocialNetwork(request.Id);
        if (deletedSocialNetwork is null)
        {
            throw new InvalidOperationException("SocialNetwork not found");
        }
        await _unitOfWork.CommitAsync();
        return _mapper.Map<SocialNetworkDto>(deletedSocialNetwork);
    }
}