using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateSpeakerCommandHandler : IRequestHandler<UpdateSpeakerCommand, Speaker>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSpeakerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Speaker> Handle(UpdateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var existingSpeaker = await _unitOfWork.SpeakerRepository.GetSpeakerById(request.Id);

        if (existingSpeaker == null)
        {
            throw new InvalidOperationException("Event not found");
        }
        existingSpeaker.Update(request.Name,
                                request.Description, 
                                request.ImageUrl,
                                request.Telephone, 
                                request.Email);
        await _unitOfWork.CommitAsync();

        return existingSpeaker;
    }
}