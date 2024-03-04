using EventPro.Domain.EventContext.Entities;

namespace EventPro.Domain.EventContext.Abstractions;

public interface ILotRepository
{
    Task<IEnumerable<Lot>> GetLotAll();
    Task<Lot> GetLotById(int lotId);
    Task<Lot> AddLot(Lot lot);
    void UpdateLot(Lot lot);
    Task<Lot> DeleteLot(int lotId);  
}