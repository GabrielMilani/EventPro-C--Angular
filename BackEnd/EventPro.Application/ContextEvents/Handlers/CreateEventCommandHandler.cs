using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Event(request.Theme,
                                 request.Local,
                                 request.Email,
                                 request.ImageUrl,
                                 request.Telephone,
                                 request.QuantityPeople, 
                                 request.EventDate);

        await _unitOfWork.EventRepository.AddEvent(newEvent);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<EventDto>(newEvent);
    }
}