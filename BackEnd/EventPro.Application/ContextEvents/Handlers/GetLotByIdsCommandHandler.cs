using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetLotByIdsCommandHandler : IRequestHandler<GetLotByIdsCommand, LotDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLotByIdsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LotDto> Handle(GetLotByIdsCommand request, CancellationToken cancellationToken)
    {
        var lot = await _unitOfWork.LotRepository.GetLotByIds(request.EventId, request.Id);
        if (lot == null) return null;

        var result = _mapper.Map<LotDto>(lot);
        return result;
    }
}