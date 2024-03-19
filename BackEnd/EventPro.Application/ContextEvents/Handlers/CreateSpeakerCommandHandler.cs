using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateSpeakerCommandHandler : IRequestHandler<CreateSpeakerCommand, SpeakerDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSpeakerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<SpeakerDto> Handle(CreateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var speaker = _mapper.Map<Speaker>(request.SpeakerAddDto);
        speaker.UserId = request.UserId;

        await _unitOfWork.SpeakerRepository.AddSpeaker(speaker);
        await _unitOfWork.CommitAsync();

        var returnSpeaker = await _unitOfWork.SpeakerRepository.GetSpeakerByUserId(request.UserId, false);
        return _mapper.Map<SpeakerDto>(returnSpeaker);
    }
}