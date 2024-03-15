using EventPro.Domain.ContextEvent.Abstractions;

namespace EventPro.Domain.ContextShared.Abstractions;

public interface IUnitOfWork
{
    IEventRepository EventRepository { get; }
    ISpeakerRepository SpeakerRepository { get; }
    ILotRepository LotRepository { get; }
    ISocialNetworkRepository SocialNetworkRepository { get; }
    IUserRepository UserRepository { get; }
    Task CommitAsync();  
}