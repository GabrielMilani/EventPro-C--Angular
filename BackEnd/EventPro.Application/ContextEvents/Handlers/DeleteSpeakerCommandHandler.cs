using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteSpeakerCommandHandler : IRequestHandler<DeleteSpeakerCommand, SpeakerDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteSpeakerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SpeakerDto> Handle(DeleteSpeakerCommand request, CancellationToken cancellationToken)
    {
        var deletedSpeaker = await _unitOfWork.SpeakerRepository.DeleteSpeaker(request.Id);
        if (deletedSpeaker is null)
        {
            throw new InvalidOperationException("Speaker not found");
        }

        await _unitOfWork.CommitAsync();
        return _mapper.Map<SpeakerDto>(deletedSpeaker);
    }
}