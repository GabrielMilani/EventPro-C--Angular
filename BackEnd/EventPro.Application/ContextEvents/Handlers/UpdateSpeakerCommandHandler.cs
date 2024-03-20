using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateSpeakerCommandHandler : IRequestHandler<UpdateSpeakerCommand, SpeakerDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSpeakerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<SpeakerDto> Handle(UpdateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var speaker = await _unitOfWork.SpeakerRepository.GetSpeakerByUserId(request.UserId, false);
        if (speaker == null) return null;

         request.SpeakerUpdateDto.UserId = speaker.UserId;
         request.SpeakerUpdateDto.Id = speaker.Id; 

        _mapper.Map(request.SpeakerUpdateDto, speaker);
        

        _unitOfWork.SpeakerRepository.UpdateSpeaker(speaker);
        await _unitOfWork.CommitAsync();

        var returnSpeaker = await _unitOfWork.SpeakerRepository.GetSpeakerByUserId(request.UserId, false);
        return _mapper.Map<SpeakerDto>(returnSpeaker);
    }
}