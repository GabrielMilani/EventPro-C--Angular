using AutoMapper;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetLotsByEventIdQuery : IRequest<LotDto[]>
{
    public int EventId { get; set; }
    public class GetLotsEventIdQueryHandler : IRequestHandler<GetLotsByEventIdQuery, LotDto[]>
    {
        private readonly ILotDapperRepository _lotDapperRepository;
        private readonly IMapper _mapper;

        public GetLotsEventIdQueryHandler(ILotDapperRepository lotDapperRepository, IMapper mapper)
        {
            _lotDapperRepository = lotDapperRepository;
            _mapper = mapper;
        }
        public async Task<LotDto[]> Handle(GetLotsByEventIdQuery request, CancellationToken cancellationToken)
        {
            var lots = await _lotDapperRepository.GetLotsByEventId(request.EventId);
            return _mapper.Map<LotDto[]>(lots);
        }
    }
}