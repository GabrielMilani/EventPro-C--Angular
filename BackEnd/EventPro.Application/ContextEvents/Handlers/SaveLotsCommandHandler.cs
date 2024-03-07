using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class SaveLotsCommandHandler : IRequestHandler<SaveLotsCommand, List<Lot>>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveLotsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Lot>> Handle(SaveLotsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var lots = await _unitOfWork.LotRepository.GetLotsByEventId(request.EventId);
            if (lots == null) return null;

            foreach (var requestLot in request.Lots)
            {
                if (requestLot.Id == 0){

                    var newLot = new Lot(requestLot.Name,
                                         requestLot.Quantity,                 
                                         requestLot.Price,
                                         requestLot.InitialDate,
                                         requestLot.FinalDate, 
                                         request.EventId,
                                         requestLot.Event);
                    await _unitOfWork.LotRepository.AddLot(newLot);
                    await _unitOfWork.CommitAsync();
                }
                else{
                    var lot = lots.FirstOrDefault(lot => lot.Id == requestLot.Id);
                    if (lot == null)
                    {
                        throw new InvalidOperationException("Lot not found");
                    }
                    lot.Update(requestLot.Name,
                               requestLot.Quantity,                 
                               requestLot.Price,
                               requestLot.InitialDate,
                               requestLot.FinalDate, 
                               requestLot.EventId,
                               requestLot.Event);
                    _unitOfWork.LotRepository.UpdateLot(lot);
                    await _unitOfWork.CommitAsync();
                }
            }
            return lots;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }  
    }
}