using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetLotByIdsQuery: IRequest<Lot>
{
    public int EventId { get; set; }
    public int Id { get; set; }

    public class GetLotByIdsQueryHandler : IRequestHandler<GetLotByIdsQuery, Lot>
    {
        private readonly ILotDapperRepository _lotDapperRepository;

        public GetLotByIdsQueryHandler(ILotDapperRepository lotDapperRepository)
            => _lotDapperRepository = lotDapperRepository;

        public async Task<Lot> Handle(GetLotByIdsQuery request, CancellationToken cancellationToken)
        {
            var lot = await _lotDapperRepository.GetLotByIds(request.EventId, request.Id);
            return lot;
        }
    }
}