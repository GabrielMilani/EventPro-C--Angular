using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateLotCommandHandler : IRequestHandler<UpdateLotCommand, Lot>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLotCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Lot> Handle(UpdateLotCommand request, CancellationToken cancellationToken)
    {
        var lot = _mapper.Map<Lot>(request.Lot);
        lot.UpdateEventId(request.EventId);
        var existingLot = await _unitOfWork.LotRepository.GetLotByIds(request.EventId, lot.Id);
        if (existingLot == null)
        {
            throw new InvalidOperationException("Lot not found");
        }
        existingLot.Update(lot.Name,
                           lot.Quantity,                 
                           lot.Price,
                           lot.InitialDate,
                           lot.FinalDate, 
                           lot.EventId,
                           lot.Event);
        await _unitOfWork.LotRepository.UpdateLot(existingLot);
        await _unitOfWork.CommitAsync();
        return existingLot;
    }
}