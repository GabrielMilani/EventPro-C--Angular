using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateSocialNetworkCommandHandler : IRequestHandler<UpdateSocialNetworkCommand, SocialNetworkDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSocialNetworkCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SocialNetworkDto> Handle(UpdateSocialNetworkCommand request, CancellationToken cancellationToken)
    {
        var existingSocialNetwork = await _unitOfWork.SocialNetworkRepository.GetSocialNetworkById(request.Id);
        if (existingSocialNetwork == null)
        {
            throw new InvalidOperationException("Member not found");
        }
        existingSocialNetwork.Update(request.Name, 
                                     request.URL, 
                                     request.EventId, 
                                     request.Event,
                                     request.SpeakerId,
                                     request.Speaker);
        _unitOfWork.SocialNetworkRepository.UpdateSocialNetwork(existingSocialNetwork);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<SocialNetworkDto>(existingSocialNetwork);
    }
}