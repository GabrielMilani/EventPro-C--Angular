using EventPro.Application.EventContext.Lot.Queries;
using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.EventContext.Entities;
using MediatR;

namespace EventPro.Application.EventContext.SocialNetwork.Queries;

public class GetSocialNetworksQuery  : IRequest<IEnumerable<Domain.EventContext.Entities.SocialNetwork>>
{
    public class GetSocialNetworkQueryHandler : IRequestHandler<GetSocialNetworksQuery, IEnumerable<Domain.EventContext.Entities.SocialNetwork>>
    {
        private readonly ISocialNetworkDapperRepository _socialNetworkDapperRepository;
        public GetSocialNetworkQueryHandler(ISocialNetworkDapperRepository socialNetworkDapperRepository)
            => _socialNetworkDapperRepository = socialNetworkDapperRepository;
        public async Task<IEnumerable<Domain.EventContext.Entities.SocialNetwork>> Handle(GetSocialNetworksQuery request, CancellationToken cancellationToken)
        {
            var socialNetworks = await _socialNetworkDapperRepository.GetSocialNetworks();
            return socialNetworks;
        }
    }
}