using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetEventByIdCommandHandler : IRequestHandler<GetEventByIdCommand, EventDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EventDto> Handle(GetEventByIdCommand request, CancellationToken cancellationToken)
    {
        var @event = await _unitOfWork.EventRepository.GetEventById(request.UserId, request.EventId);
        return _mapper.Map<EventDto>(@event);
    }
}