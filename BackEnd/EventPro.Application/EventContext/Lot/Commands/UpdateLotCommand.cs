using EventPro.Application.EventContext.Lot.Commands;
using EventPro.Domain.SharedContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.Lot.Commands;

public class UpdateLotCommand : LotCommandBase
{
    public int Id { get; set; }  

    public class UpdateLotCommandHandler : IRequestHandler<UpdateLotCommand, Domain.EventContext.Entities.Lot>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLotCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Domain.EventContext.Entities.Lot> Handle(UpdateLotCommand request, CancellationToken cancellationToken)
        {
            var existingLot = await _unitOfWork.LotRepository.GetLotById(request.Id);

            if (existingLot == null)
            {
                throw new InvalidOperationException("Lot not found");
            }
            existingLot.Update(request.Name,
                               request.Price, 
                               request.InitialDate,
                               request.FinalDate, 
                               request.Quantity,
                               request.EventId,
                               request.Event);
            _unitOfWork.LotRepository.UpdateLot(existingLot);
            await _unitOfWork.CommitAsync();

            return existingLot;
        }
    }
}