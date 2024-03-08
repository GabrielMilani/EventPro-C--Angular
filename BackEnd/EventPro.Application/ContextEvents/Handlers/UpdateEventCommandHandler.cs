using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Event> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var existingEvent = await _unitOfWork.EventRepository.GetEventById(request.Id);

        if (existingEvent == null)
        {
            throw new InvalidOperationException("Event not found");
        }
        existingEvent.Update(request.Theme,
                             request.Local,
                             request.Email,
                             request.ImageUrl,
                             request.Telephone,
                             request.QuantityPeople, 
                             request.EventDate);
        _unitOfWork.EventRepository.UpdateEvent(existingEvent);
        await _unitOfWork.CommitAsync();

        return existingEvent;
    }
}