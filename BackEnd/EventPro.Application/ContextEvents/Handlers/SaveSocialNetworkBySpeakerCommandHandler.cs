using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class SaveSocialNetworkBySpeakerCommandHandler : IRequestHandler<SaveSocialNetworkBySpeakerCommand, SocialNetworkDto[]>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SaveSocialNetworkBySpeakerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<SocialNetworkDto[]> Handle(SaveSocialNetworkBySpeakerCommand request, CancellationToken cancellationToken)
    {
        var socialNetworkList = await _unitOfWork.SocialNetworkRepository.GetSocialNetworksBySpeakerId(request.SpeakerId);
        if (socialNetworkList == null) return null;

        foreach (var requestSocialNetwork in request.SocialNetworkDto)
        {
            if (requestSocialNetwork.Id == 0 || requestSocialNetwork.Id == null )
            {
                var command = new CreateSocialNetworkBySpeakerCommand();
                command.SocialNetworkDto = _mapper.Map<SocialNetworkDto>(requestSocialNetwork);
                command.SpeakerId = request.SpeakerId;
                await _mediator.Send(command);
            }
            else
            {
                var command = new UpdateSocialNetworkSpeakerIdCommand();
                command.SocialNetworkDto = _mapper.Map<SocialNetworkDto>(requestSocialNetwork);
                command.SpeakerId = request.SpeakerId;
                await _mediator.Send(command);
            }
        }
        var commandReturn = new GetSocialNetworksSpeakerCommand
        {
            SpeakerId = request.SpeakerId
        };
        var socialNetworkReturn = await _mediator.Send(commandReturn);
        return _mapper.Map<SocialNetworkDto[]>(socialNetworkReturn);
    }
}