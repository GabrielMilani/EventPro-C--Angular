using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetEventsByThemeCommandHandler : IRequestHandler<GetEventsByThemeCommand, EventDto[]>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventsByThemeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EventDto[]> Handle(GetEventsByThemeCommand request, CancellationToken cancellationToken)
    {
        var eventList = await _unitOfWork.EventRepository.GetEventsByTheme(request.UserId, request.Theme);
        return _mapper.Map<EventDto[]>(eventList);
    }
}