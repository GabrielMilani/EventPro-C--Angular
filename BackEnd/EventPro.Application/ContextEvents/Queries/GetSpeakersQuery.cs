using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetSpeakersQuery : IRequest<IEnumerable<Speaker>>
{
    public class GetLotQueryHandler : IRequestHandler<GetSpeakersQuery, IEnumerable<Speaker>>
    {
        private readonly ISpeakerDapperRepository _speakerDapperRepository;
        public GetLotQueryHandler(ISpeakerDapperRepository speakerDapperRepository)
            => _speakerDapperRepository = speakerDapperRepository;
        public async Task<IEnumerable<Speaker>> Handle(GetSpeakersQuery request, CancellationToken cancellationToken)
        {
            var speakers = await _speakerDapperRepository.GetSpeakers();
            return speakers;
        }
    }
}