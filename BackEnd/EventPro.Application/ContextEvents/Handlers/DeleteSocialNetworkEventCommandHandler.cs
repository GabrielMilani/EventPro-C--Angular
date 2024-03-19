using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteSocialNetworkEventCommandHandler : IRequestHandler<DeleteSocialNetworkEventCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteSocialNetworkEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteSocialNetworkEventCommand request, CancellationToken cancellationToken)
    {
        var socialNetwork = await _unitOfWork.SocialNetworkRepository.DeleteSocialNetworkEventId(request.EventId, request.SocialNetworkId);
        if (socialNetwork == null) return false;
        await _unitOfWork.CommitAsync();
        return true;
    }
}