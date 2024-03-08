using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Infrastructure.Context.ContextEvent.Repositories;

public class LotRepository : ILotRepository
{
    private readonly AppDbContext _context;

    public LotRepository(AppDbContext context)
        => _context = context;

    public async Task<Lot> AddLot(Lot lot)
    {
        if (lot is null)
            throw new ArgumentNullException(nameof(lot));
        await _context.Lots.AddAsync(lot);
        return lot;
    }

    public async Task<Lot> UpdateLot(Lot lot)
    {
        if (lot is null)
            throw new ArgumentNullException(nameof(lot));
        _context.Lots.Update(lot);
        return lot;
    }

    // Aula de angular!
    public async Task<Lot[]> GetLotsByEventId(int eventId)
    {
        IQueryable<Lot> query = _context.Lots;
        query = query.AsNoTracking().Where(lot => lot.EventId == eventId);
        return await query.ToArrayAsync();
    }

    public async Task<Lot> GetLotByIds(int eventId, int id)
    {
        IQueryable<Lot> query = _context.Lots;
        query = query.AsNoTracking().Where(lot => lot.EventId == eventId && lot.Id == id);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<Lot> DeleteLotByIds(int eventId, int id)
    {
        var lot = await GetLotByIds(eventId, id);
        if (lot is null)
            throw new InvalidOperationException("Event not found");
        _context.Lots.Remove(lot);
        return lot;
    }
}