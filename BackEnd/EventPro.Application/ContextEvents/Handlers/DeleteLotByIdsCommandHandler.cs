using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class DeleteLotByIdsCommandHandler : IRequestHandler<DeleteLotByIdsCommand, LotDto>
{
    private readonly IUnitOfWork _unitOfWork; 
    private readonly IMapper _mapper;

    public DeleteLotByIdsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LotDto> Handle(DeleteLotByIdsCommand request, CancellationToken cancellationToken)
    {
        var lot = await _unitOfWork.LotRepository.DeleteLotByIds(request.EventId, request.Id);
        if (lot == null) return null;

        return _mapper.Map<LotDto>(lot);
    }
}