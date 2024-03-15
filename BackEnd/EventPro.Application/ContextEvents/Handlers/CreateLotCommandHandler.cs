using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateLotCommandHandler : IRequestHandler<CreateLotCommand, Lot>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLotCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Lot> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        var newLot = _mapper.Map<Lot>(request.Lot);
        newLot.EventId = request.EventId;
        await _unitOfWork.LotRepository.AddLot(newLot);
        await _unitOfWork.CommitAsync();
        return  newLot;
    }
}