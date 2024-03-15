using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetEventsCommandHandler : IRequestHandler<GetEventsCommand, EventDto[]>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EventDto[]> Handle(GetEventsCommand request, CancellationToken cancellationToken)
    {
        var eventList = await _unitOfWork.EventRepository.GetEvents(request.UserId);
        return _mapper.Map<EventDto[]>(eventList);
    }
}