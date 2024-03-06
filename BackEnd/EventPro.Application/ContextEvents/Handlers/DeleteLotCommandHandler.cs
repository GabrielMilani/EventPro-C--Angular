using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteLotCommandHandler : IRequestHandler<DeleteLotCommand, Lot>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLotCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Lot> Handle(DeleteLotCommand request, CancellationToken cancellationToken)
    {
        var deletedLot = await _unitOfWork.LotRepository.DeleteLot(request.Id);

        if (deletedLot is null)
        {
            throw new InvalidOperationException("Lot not found");
        }

        await _unitOfWork.CommitAsync();
        return deletedLot;
    }
}