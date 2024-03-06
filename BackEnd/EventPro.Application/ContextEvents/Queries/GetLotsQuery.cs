using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetLotsQuery : IRequest<IEnumerable<Lot>>
{
    public class GetLotQueryHandler : IRequestHandler<GetLotsQuery, IEnumerable<Lot>>
    {
        private readonly ILotDapperRepository _lotDapperRepository;
        public GetLotQueryHandler(ILotDapperRepository lotDapperRepository)
            => _lotDapperRepository = lotDapperRepository;
        public async Task<IEnumerable<Lot>> Handle(GetLotsQuery request, CancellationToken cancellationToken)
        {
            var lots = await _lotDapperRepository.GetLots();
            return lots;
        }
    }
}