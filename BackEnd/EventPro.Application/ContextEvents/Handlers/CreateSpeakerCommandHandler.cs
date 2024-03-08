using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateSpeakerCommandHandler : IRequestHandler<CreateSpeakerCommand, SpeakerDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateSpeakerCommandHandler(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SpeakerDto> Handle(CreateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var newSpeaker = _mapper.Map<Speaker>(request);
        await _unitOfWork.SpeakerRepository.AddSpeaker(newSpeaker);
        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<SpeakerDto>(newSpeaker);
        return result;
    }
}