using EventPro.Application.EventContext.Event.Queries;
using EventPro.Domain.EventContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.Lot.Queries;

public class GetLotByIdQuery: IRequest<Domain.EventContext.Entities.Lot>
{
    public int Id { get; set; }

    public class GetLotByIdQueryHandler : IRequestHandler<GetLotByIdQuery, Domain.EventContext.Entities.Lot>
    {
        private readonly ILotDapperRepository _lotDapperRepository;

        public GetLotByIdQueryHandler(ILotDapperRepository lotDapperRepository)
            => _lotDapperRepository = lotDapperRepository;

        public async Task<Domain.EventContext.Entities.Lot> Handle(GetLotByIdQuery request, CancellationToken cancellationToken)
        {
            var lot = await _lotDapperRepository.GetLotById(request.Id);
            return lot;
        }
    }
}