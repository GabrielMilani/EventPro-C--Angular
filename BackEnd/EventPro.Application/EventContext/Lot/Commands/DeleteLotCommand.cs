using EventPro.Domain.SharedContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.Lot.Commands;

public class DeleteLotCommand : IRequest<Domain.EventContext.Entities.Lot>
{
public int Id { get; set; }

public class DeleteLotCommandHandler : IRequestHandler<DeleteLotCommand, Domain.EventContext.Entities.Lot>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLotCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Domain.EventContext.Entities.Lot> Handle(DeleteLotCommand request, CancellationToken cancellationToken)
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
}