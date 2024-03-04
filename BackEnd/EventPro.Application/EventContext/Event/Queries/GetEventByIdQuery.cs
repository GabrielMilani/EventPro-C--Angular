using EventPro.Domain.EventContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.Event.Queries;

public class GetEventByIdQuery : IRequest<Domain.EventContext.Entities.Event>
{
    public int Id { get; set; }

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Domain.EventContext.Entities.Event>
    {
        private readonly IEventDapperRepository _eventDapperRepository;

        public GetEventByIdQueryHandler(IEventDapperRepository eventDapperRepository)
            => _eventDapperRepository = eventDapperRepository;

        public async Task<Domain.EventContext.Entities.Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventDapperRepository.GetEventById(request.Id);
            return @event;
        }
    }
}