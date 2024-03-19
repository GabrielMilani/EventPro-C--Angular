using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateSocialNetworkByEventCommandHandler : IRequestHandler<CreateSocialNetworkByEventCommand, SocialNetwork>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSocialNetworkByEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SocialNetwork> Handle(CreateSocialNetworkByEventCommand request, CancellationToken cancellationToken)
    {
        var newSocialNetwork = _mapper.Map<SocialNetwork>(request.SocialNetworkDto);
        newSocialNetwork.EventId = request.EventId;
        await _unitOfWork.SocialNetworkRepository.AddSocialNetwork(newSocialNetwork);
        await _unitOfWork.CommitAsync();
        return  newSocialNetwork;
    }
}