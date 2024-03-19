using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class SaveSocialNetworkByEventCommandHandler : IRequestHandler<SaveSocialNetworkByEventCommand, SocialNetworkDto[]>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SaveSocialNetworkByEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<SocialNetworkDto[]> Handle(SaveSocialNetworkByEventCommand request, CancellationToken cancellationToken)
    {
        var socialNetworkList = await _unitOfWork.SocialNetworkRepository.GetSocialNetworksByEventId(request.EventId);
        if (socialNetworkList == null) return null;

        foreach (var requestSocialNetwork in request.SocialNetworkDto)
        {
            if (requestSocialNetwork.Id == 0 || requestSocialNetwork.Id == null )
            {
                var command = new CreateSocialNetworkByEventCommand();
                command.SocialNetworkDto = _mapper.Map<SocialNetworkDto>(requestSocialNetwork);
                command.EventId = request.EventId;
                await _mediator.Send(command);
            }
            else
            {
                var command = new UpdateSocialNetworkEventIdCommand();
                command.SocialNetworkDto = _mapper.Map<SocialNetworkDto>(requestSocialNetwork);
                command.EventId = request.EventId;
                await _mediator.Send(command);
            }
        }
        var commandReturn = new GetSocialNetworksEventCommand
        {
            EventId = request.EventId
        };
        var socialNetworkReturn = await _mediator.Send(commandReturn);
        return _mapper.Map<SocialNetworkDto[]>(socialNetworkReturn);
    }
}