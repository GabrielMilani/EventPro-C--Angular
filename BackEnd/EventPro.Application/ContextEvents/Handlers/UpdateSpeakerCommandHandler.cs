using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateSpeakerCommandHandler : IRequestHandler<UpdateSpeakerCommand, SpeakerDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSpeakerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SpeakerDto> Handle(UpdateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var existingSpeaker = await _unitOfWork.SpeakerRepository.GetSpeakerById(request.Id);
        if (existingSpeaker == null)
        {
            throw new InvalidOperationException("Event not found");
        }
        existingSpeaker.Update(request.Name,
                               request.Description, 
                               request.Telephone, 
                               request.Email,
                               request.ImageUrl);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<SpeakerDto>(existingSpeaker);
    }
}