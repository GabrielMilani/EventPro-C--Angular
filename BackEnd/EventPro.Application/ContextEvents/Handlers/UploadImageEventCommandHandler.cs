using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UploadImageEventCommandHandler : IRequestHandler<UploadImageEventCommand, EventDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UploadImageEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EventDto> Handle(UploadImageEventCommand request, CancellationToken cancellationToken)
    {
        var @event = _mapper.Map<Event>(request.EventDto);
        _unitOfWork.EventRepository.UpdateEvent(@event);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<EventDto>(@event);
    }
}