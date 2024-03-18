using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using EventPro.Domain.ContextShared.Models;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetEventsCommandHandler : IRequestHandler<GetEventsCommand, PageList<EventDto> >
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PageList<EventDto>> Handle(GetEventsCommand request, CancellationToken cancellationToken)
    {
        var eventList = await _unitOfWork.EventRepository.GetEvents(request.UserId, request.PageParams, request.IncludeSpeaker);
        
        var result = _mapper.Map<PageList<EventDto>>(eventList);

        result.CurrentPage = eventList.CurrentPage;
        result.PageSize = eventList.PageSize;
        result.TotalCount = eventList.TotalCount;
        result.TotalPages = eventList.TotalPages;

        return result;
    }
}