using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.ContextEvents.Queries;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class SaveLotsCommandHandler : IRequestHandler<SaveLotsCommand, LotDto[]>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SaveLotsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<LotDto[]> Handle(SaveLotsCommand request, CancellationToken cancellationToken)
    {

        var lots = await _unitOfWork.LotRepository.GetLotsByEventId(request.EventId);
        if (lots == null) return null;

        foreach (var requestLot in request.Lots)
        {
            if (requestLot.Id == 0 || requestLot.Id == null )
            {
                var command = new CreateLotCommand();
                command.Lot = _mapper.Map<LotDto>(requestLot);
                command.EventId = request.EventId;
                await _mediator.Send(command);
            }
            else
            {
               var command = new UpdateLotCommand();
               command.Lot = _mapper.Map<LotDto>(requestLot);
               command.EventId = request.EventId;
               await _mediator.Send(command);
            }
        }
        var commandReturn = new GetLotsByEventIdQuery();
        commandReturn.EventId = request.EventId; 
        var lotReturn = await _mediator.Send(commandReturn);
        return _mapper.Map<LotDto[]>(lotReturn);
    }
}