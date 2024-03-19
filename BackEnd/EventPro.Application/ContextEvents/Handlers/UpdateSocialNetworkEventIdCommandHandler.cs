using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateSocialNetworkEventIdCommandHandler : IRequestHandler<UpdateSocialNetworkEventIdCommand, SocialNetwork>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSocialNetworkEventIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SocialNetwork> Handle(UpdateSocialNetworkEventIdCommand request, CancellationToken cancellationToken)
    {
        var socialNetwork = _mapper.Map<SocialNetwork>(request.SocialNetworkDto);
        socialNetwork.EventId = request.EventId;
        var existingSocialNetwork = await _unitOfWork.SocialNetworkRepository.GetSocialNetworkEventByIds(request.EventId, socialNetwork.Id);
        if (existingSocialNetwork == null)
        {
            throw new InvalidOperationException("Lot not found");
        }
        _mapper.Map(request, existingSocialNetwork);
        _unitOfWork.SocialNetworkRepository.UpdateSocialNetwork(existingSocialNetwork);
        await _unitOfWork.CommitAsync();
        return existingSocialNetwork;
    }
}