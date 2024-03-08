using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetLotsByEventIdQuery : IRequest<IEnumerable<Lot>>
{
    public int EventId { get; set; }
    public class GetLotsEventIdQueryHandler : IRequestHandler<GetLotsByEventIdQuery, IEnumerable<Lot>>
    {
        private readonly ILotDapperRepository _lotDapperRepository;
        public GetLotsEventIdQueryHandler(ILotDapperRepository lotDapperRepository)
            => _lotDapperRepository = lotDapperRepository;
        public async Task<IEnumerable<Lot>> Handle(GetLotsByEventIdQuery request, CancellationToken cancellationToken)
        {
            var lots = await _lotDapperRepository.GetLotsByEventId(request.EventId);
            return lots;
        }
    }
}