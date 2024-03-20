using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetSpeakerByUserIdCommandHandler : IRequestHandler<GetSpeakerByUserIdCommand, SpeakerDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSpeakerByUserIdCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<SpeakerDto> Handle(GetSpeakerByUserIdCommand request, CancellationToken cancellationToken)
    {
        var speaker = await _unitOfWork.SpeakerRepository.GetSpeakerByUserId(request.UserId, request.IncludeEvents);

        return _mapper.Map<SpeakerDto>(speaker);
    }
}