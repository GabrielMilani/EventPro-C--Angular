using AutoMapper;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetEventByIdQuery : IRequest<EventDto>
{
    public int Id { get; set; }

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDto>
    {
        private readonly IEventDapperRepository _eventDapperRepository;
        private readonly IMapper _mapper;

        public GetEventByIdQueryHandler(IEventDapperRepository eventDapperRepository, IMapper mapper)
        {
            _eventDapperRepository = eventDapperRepository;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var eventDto = _mapper.Map<EventDto>(await _eventDapperRepository.GetEventById(request.Id));
            return eventDto;
        }
    }
}