using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.EventContext.Entities;
using MediatR;

namespace EventPro.Application.EventContext.Event.Queries;

public class GetEventsQuery : IRequest<IEnumerable<Domain.EventContext.Entities.Event>>
{
    public class GetEventQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<Domain.EventContext.Entities.Event>>
    {
        private readonly IEventDapperRepository _eventDapperRepository;
        public GetEventQueryHandler(IEventDapperRepository eventDapperRepository)
            => _eventDapperRepository = eventDapperRepository;
        public async Task<IEnumerable<Domain.EventContext.Entities.Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventDapperRepository.GetEvents();
            return events;
        }
    }
}