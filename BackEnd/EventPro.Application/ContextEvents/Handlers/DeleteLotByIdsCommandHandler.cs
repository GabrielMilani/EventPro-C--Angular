using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteLotByIdsCommandHandler : IRequestHandler<DeleteLotByIdsCommand, Lot>
{
    private readonly IUnitOfWork _unitOfWork;
    public async Task<Lot> Handle(DeleteLotByIdsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var lot = await _unitOfWork.LotRepository.DeleteLotByIds(request.Id, request.EventId);
            if (lot == null) return null;

            return lot;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }  
    }
}