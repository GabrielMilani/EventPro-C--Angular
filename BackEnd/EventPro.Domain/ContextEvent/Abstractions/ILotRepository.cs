using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ILotRepository
{
    Task<IEnumerable<Lot>> GetLots();
    Task<Lot> GetLotById(int lotId);
    Task<Lot> AddLot(Lot lot);
    void UpdateLot(Lot lot);
    Task<Lot> DeleteLot(int lotId);  
}  