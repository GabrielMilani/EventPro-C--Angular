using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.SharedContext.Abstractions;
using EventPro.Infrastructure.Context.EventContext.Repositories;
using EventPro.Infrastructure.Data;

namespace EventPro.Infrastructure.Context.SharedContext;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IEventRepository? _eventRepository;
    private ISpeakerRepository? _speakerRepository;
    private ILotRepository? _lotRepository;
    private ISocialNetworkRepository? _socialNetworkRepository;

    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
        =>  _context = context;

    public IEventRepository EventRepository  
        => _eventRepository = _eventRepository ?? new EventRepository(_context);
    public ISpeakerRepository SpeakerRepository 
        => _speakerRepository = _speakerRepository ?? new SpeakerRepository(_context);
    public ILotRepository LotRepository 
        => _lotRepository = _lotRepository ?? new LotRepository(_context);
    public ISocialNetworkRepository SocialNetworkRepository 
        => _socialNetworkRepository = _socialNetworkRepository ?? new SocialNetworkRepository(_context);
    
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}