using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using MediatR;

namespace EventPro.Application.ContextEvents.Queries;

public class GetSocialNetworksQuery  : IRequest<IEnumerable<SocialNetwork>>
{
    public class GetSocialNetworkQueryHandler : IRequestHandler<GetSocialNetworksQuery, IEnumerable<SocialNetwork>>
    {
        private readonly ISocialNetworkDapperRepository _socialNetworkDapperRepository;
        public GetSocialNetworkQueryHandler(ISocialNetworkDapperRepository socialNetworkDapperRepository)
            => _socialNetworkDapperRepository = socialNetworkDapperRepository;
        public async Task<IEnumerable<SocialNetwork>> Handle(GetSocialNetworksQuery request, CancellationToken cancellationToken)
        {
            var socialNetworks = await _socialNetworkDapperRepository.GetSocialNetworks();
            return socialNetworks;
        }
    }
}