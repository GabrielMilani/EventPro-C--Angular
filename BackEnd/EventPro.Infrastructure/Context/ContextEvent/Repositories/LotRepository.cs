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

    public async Task<IEnumerable<Lot>> GetLots()
    {
        var lotList = await _context.Lots.ToListAsync();
        return lotList ?? Enumerable.Empty<Lot>();
    }

    public async Task<Lot> GetLotById(int lotId)
    {
        var lot = await _context.Lots.FindAsync(lotId);
        if (lot is null)
            throw new InvalidOperationException("Lot not found");
        return lot;
    }

    public async Task<Lot> AddLot(Lot lot)
    {
        if (lot is null)
            throw new ArgumentNullException(nameof(lot));
        await _context.Lots.AddAsync(lot);
        return lot;
    }

    public void UpdateLot(Lot lot)
    {
        if (lot is null)
            throw new ArgumentNullException(nameof(lot));
        _context.Lots.Update(lot);
    }

    public async Task<Lot> DeleteLot(int lotId)
    {
        var lot = await GetLotById(lotId);
        if (lot is null)
            throw new InvalidOperationException("Event not found");
        _context.Lots.Remove(lot);
        return lot;
    }
}