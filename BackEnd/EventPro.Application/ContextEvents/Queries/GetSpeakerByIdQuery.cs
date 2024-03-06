using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetSpeakerByIdQuery: IRequest<Speaker>
{
    public int Id { get; set; }

    public class GetSpeakerByIdQueryHandler : IRequestHandler<GetSpeakerByIdQuery, Speaker>
    {
        private readonly ISpeakerDapperRepository _speakerDapperRepository;

        public GetSpeakerByIdQueryHandler(ISpeakerDapperRepository speakerDapperRepository)
            => _speakerDapperRepository = speakerDapperRepository;

        public async Task<Speaker> Handle(GetSpeakerByIdQuery request, CancellationToken cancellationToken)
        {
            var speaker = await _speakerDapperRepository.GetSpeakerById(request.Id);
            return speaker;
        }
    }
}