using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using EventPro.Domain.ContextShared.Models;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetSpeakersCommandHandler : IRequestHandler<GetSpeakersCommand, PageList<SpeakerDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSpeakersCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PageList<SpeakerDto>> Handle(GetSpeakersCommand request, CancellationToken cancellationToken)
    {
        var speakerList = await _unitOfWork.SpeakerRepository.GetSpeakers(request.PageParams, request.IncludeEvents);
        if (speakerList == null) return null;
        
        var result = _mapper.Map<PageList<SpeakerDto>>(speakerList);

        result.CurrentPage = speakerList.CurrentPage;
        result.PageSize = speakerList.PageSize;
        result.TotalCount = speakerList.TotalCount;
        result.TotalPages = speakerList.TotalPages;

        return result;
    }
}