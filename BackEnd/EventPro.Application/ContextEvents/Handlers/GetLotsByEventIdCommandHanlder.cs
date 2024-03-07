using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetLotsByEventIdCommandHanlder : IRequestHandler<GetLotsByEventIdCommand, List<Lot>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLotsByEventIdCommandHanlder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Lot>> Handle(GetLotsByEventIdCommand request, CancellationToken cancellationToken)
    {
        List<Lot> lots;
        try
        {
            lots = await _unitOfWork.LotRepository.GetLotsByEventId(request.EventId);
            if (lots == null) return null;

            return lots;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }  
    }
}