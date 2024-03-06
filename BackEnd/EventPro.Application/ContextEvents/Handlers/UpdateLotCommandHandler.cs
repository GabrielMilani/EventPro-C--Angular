using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateLotCommandHandler : IRequestHandler<UpdateLotCommand, Lot>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLotCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Lot> Handle(UpdateLotCommand request, CancellationToken cancellationToken)
    {
        var existingLot = await _unitOfWork.LotRepository.GetLotById(request.Id);

        if (existingLot == null)
        {
            throw new InvalidOperationException("Lot not found");
        }
        existingLot.Update(request.Name,
                            request.Quantity,                 
                            request.Price,
                            request.InitialDate,
                            request.FinalDate, 
                            request.EventId,
                            request.Event);
        _unitOfWork.LotRepository.UpdateLot(existingLot);
        await _unitOfWork.CommitAsync();

        return existingLot;
    }
}