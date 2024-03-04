using EventPro.Domain.EventContext.Abstractions;

namespace EventPro.Domain.SharedContext.Abstractions;

public interface IUnitOfWork
{ 
    IEventRepository EventRepository { get; }
    ISpeakerRepository SpeakerRepository { get; }
    ILotRepository LotRepository { get; }
    ISocialNetworkRepository SocialNetworkRepository { get; }
    Task CommitAsync();
}