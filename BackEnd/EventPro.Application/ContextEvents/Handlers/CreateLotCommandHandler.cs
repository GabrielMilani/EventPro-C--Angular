using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateLotCommandHandler : IRequestHandler<CreateLotCommand, Lot>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLotCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Lot> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        var newLot = new Lot(request.Name,
                             request.Quantity,                 
                             request.Price,
                             request.InitialDate,
                             request.FinalDate, 
                             request.EventId,
                             request.Event);
        await _unitOfWork.LotRepository.AddLot(newLot);
        await _unitOfWork.CommitAsync();

        return newLot;
    }
}