using EventPro.Application.EventContext.Lot.Queries;
using EventPro.Domain.EventContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.SocialNetwork.Queries;

public class GetSocialNetworkByIdQuery : IRequest<Domain.EventContext.Entities.SocialNetwork>
{
    public int Id { get; set; }

    public class GetSocialNetworkByIdQueryHandler : IRequestHandler<GetSocialNetworkByIdQuery, Domain.EventContext.Entities.SocialNetwork>
    {
        private readonly ISocialNetworkDapperRepository _socialNetworkDapperRepository;

        public GetSocialNetworkByIdQueryHandler(ISocialNetworkDapperRepository socialNetworkDapperRepository)
            => _socialNetworkDapperRepository = socialNetworkDapperRepository;

        public async Task<Domain.EventContext.Entities.SocialNetwork> Handle(GetSocialNetworkByIdQuery request, CancellationToken cancellationToken)
        {
            var socialNetwork = await _socialNetworkDapperRepository.GetSocialNetworkById(request.Id);
            return socialNetwork;
        }
    }
}