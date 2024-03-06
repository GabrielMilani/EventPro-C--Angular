using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetEventsQuery : IRequest<IEnumerable<Event>>
{
    public class GetEventQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<Event>>
    {
        private readonly IEventDapperRepository _eventDapperRepository;
        public GetEventQueryHandler(IEventDapperRepository eventDapperRepository)
            => _eventDapperRepository = eventDapperRepository;
        public async Task<IEnumerable<Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventDapperRepository.GetEvents();
            return events;
        }
    }
}