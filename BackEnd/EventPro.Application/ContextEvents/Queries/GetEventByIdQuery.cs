using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetEventByIdQuery : IRequest<Event>
{
    public int Id { get; set; }

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly IEventDapperRepository _eventDapperRepository;

        public GetEventByIdQueryHandler(IEventDapperRepository eventDapperRepository)
            => _eventDapperRepository = eventDapperRepository;

        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventDapperRepository.GetEventById(request.Id);
            return @event;
        }
    }
}