using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetLotByIdsCommandHandler : IRequestHandler<GetLotByIdsCommand, Lot>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLotByIdsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Lot> Handle(GetLotByIdsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var lot = await _unitOfWork.LotRepository.GetLotByIds(request.EventId,
                                                                  request.Id);
            if (lot == null) return null;

            return lot;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}