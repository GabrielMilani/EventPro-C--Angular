using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetLotsByEventIdCommandHanlder : IRequestHandler<GetLotsByEventIdCommand, LotDto[]>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetLotsByEventIdCommandHanlder(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LotDto[]> Handle(GetLotsByEventIdCommand request, CancellationToken cancellationToken)
    {
        var lots = await _unitOfWork.LotRepository.GetLotsByEventId(request.EventId);
        if (lots == null) return null;

        var result = _mapper.Map<LotDto[]>(lots);
        return result;
    }
}