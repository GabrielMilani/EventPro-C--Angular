using EventPro.Application.EventContext.Lot.Queries;
using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.EventContext.Entities;
using MediatR;

namespace EventPro.Application.EventContext.Speaker.Queries;

public class GetSpeakerByIdQuery: IRequest<Domain.EventContext.Entities.Speaker>
{
    public int Id { get; set; }

    public class GetSpeakerByIdQueryHandler : IRequestHandler<GetSpeakerByIdQuery, Domain.EventContext.Entities.Speaker>
    {
        private readonly ISpeakerDapperRepository _speakerDapperRepository;

        public GetSpeakerByIdQueryHandler(ISpeakerDapperRepository speakerDapperRepository)
            => _speakerDapperRepository = speakerDapperRepository;

        public async Task<Domain.EventContext.Entities.Speaker> Handle(GetSpeakerByIdQuery request, CancellationToken cancellationToken)
        {
            var speaker = await _speakerDapperRepository.GetSpeakerById(request.Id);
            return speaker;
        }
    }
}