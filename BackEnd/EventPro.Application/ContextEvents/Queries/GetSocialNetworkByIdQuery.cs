using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetSocialNetworkByIdQuery : IRequest<SocialNetwork>
{
    public int Id { get; set; }

    public class GetSocialNetworkByIdQueryHandler : IRequestHandler<GetSocialNetworkByIdQuery, SocialNetwork>
    {
        private readonly ISocialNetworkDapperRepository _socialNetworkDapperRepository;

        public GetSocialNetworkByIdQueryHandler(ISocialNetworkDapperRepository socialNetworkDapperRepository)
            => _socialNetworkDapperRepository = socialNetworkDapperRepository;

        public async Task<SocialNetwork> Handle(GetSocialNetworkByIdQuery request, CancellationToken cancellationToken)
        {
            var socialNetwork = await _socialNetworkDapperRepository.GetSocialNetworkById(request.Id);
            return socialNetwork;
        }
    }
}