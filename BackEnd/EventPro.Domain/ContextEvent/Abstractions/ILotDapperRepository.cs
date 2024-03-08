using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ILotDapperRepository
{
    Task<IEnumerable<Lot>> GetLots();
    Task<Lot?> GetLotById(int lotId);

    Task<IEnumerable<Lot>> GetLotsByEventId(int eventId);
    Task<Lot?> GetLotByIds(int eventId, int lotId);
}