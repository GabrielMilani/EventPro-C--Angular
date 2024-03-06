using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetLotByIdQuery: IRequest<Lot>
{
    public int Id { get; set; }

    public class GetLotByIdQueryHandler : IRequestHandler<GetLotByIdQuery, Lot>
    {
        private readonly ILotDapperRepository _lotDapperRepository;

        public GetLotByIdQueryHandler(ILotDapperRepository lotDapperRepository)
            => _lotDapperRepository = lotDapperRepository;

        public async Task<Lot> Handle(GetLotByIdQuery request, CancellationToken cancellationToken)
        {
            var lot = await _lotDapperRepository.GetLotById(request.Id);
            return lot;
        }
    }
}