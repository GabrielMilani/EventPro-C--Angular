using EventPro.Application.EventContext.Event.Commands;
using EventPro.Domain.SharedContext.Abstractions;
using MediatR;

namespace EventPro.Application.EventContext.Speaker.Commands;

public class UpdateSpeakerCommand : SpeakerCommandBase
{
    public int Id { get; set; }  

    public class UpdateSpeakerCommandHandler : IRequestHandler<UpdateSpeakerCommand, Domain.EventContext.Entities.Speaker>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSpeakerCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Domain.EventContext.Entities.Speaker> Handle(UpdateSpeakerCommand request, CancellationToken cancellationToken)
        {
            var existingSpeaker = await _unitOfWork.SpeakerRepository.GetSpeakerById(request.Id);

            if (existingSpeaker == null)
            {
                throw new InvalidOperationException("Event not found");
            }
            existingSpeaker.Update(request.Name,
                                   request.MiniCV, 
                                   request.ImageUrl,
                                   request.Telephone, 
                                   request.Email,
                                   request.SocialNetworks,
                                   request.SpeakerEvents);
            await _unitOfWork.CommitAsync();

            return existingSpeaker;
        }
    }
}