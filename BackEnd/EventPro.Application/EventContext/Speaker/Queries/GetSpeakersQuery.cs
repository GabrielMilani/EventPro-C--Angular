using EventPro.Application.EventContext.Lot.Queries;
using EventPro.Domain.EventContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.Speaker.Queries;

public class GetSpeakersQuery : IRequest<IEnumerable<Domain.EventContext.Entities.Lot>>
{
    public class GetLotQueryHandler : IRequestHandler<GetLotsQuery, IEnumerable<Domain.EventContext.Entities.Lot>>
    {
        private readonly ILotDapperRepository _lotDapperRepository;
        public GetLotQueryHandler(ILotDapperRepository lotDapperRepository)
            => _lotDapperRepository = lotDapperRepository;
        public async Task<IEnumerable<Domain.EventContext.Entities.Lot>> Handle(GetLotsQuery request, CancellationToken cancellationToken)
        {
            var lots = await _lotDapperRepository.GetLots();
            return lots;
        }
    }
}