using EventPro.Domain.EventContext.Entities;

namespace EventPro.Domain.EventContext.Abstractions;

public interface ILotDapperRepository
{
    Task<IEnumerable<Lot>> GetLots();
    Task<Lot?> GetLotById(int lotId);  
}