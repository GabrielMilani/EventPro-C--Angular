using EventPro.Application.EventContext.Lot.Commands;
using EventPro.Domain.SharedContext.Abstractions;

namespace EventPro.Application.EventContext.Lot.Commands;

public class CreateLotCommand : LotCommandBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLotCommand(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    public async Task<Domain.EventContext.Entities.Lot> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        var newLot = new Domain.EventContext.Entities.Lot(request.Name,
                                                          request.EventId, 
                                                          request.InitialDate,
                                                          request.FinalDate, 
                                                          request.Quantity,
                                                          request.EventId,
                                                          request.Event);
        await _unitOfWork.LotRepository.AddLot(newLot);
        await _unitOfWork.CommitAsync();

        return newLot;
    } 
}