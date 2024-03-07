using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ILotRepository
{
    Task<List<Lot>> GetLotsByEventId(int eventId);
    Task<Lot> GetLotByIds(int eventId, int id);
    Task<Lot> DeleteLotByIds(int eventId, int id);
    Task<List<Lot>> SaveLots(int eventId, List<Lot> listLots);

    Task<IEnumerable<Lot>> GetLots();
    Task<Lot> GetLotById(int lotId);
    Task<Lot> AddLot(Lot lot);
    void UpdateLot(Lot lot);
    Task<Lot> DeleteLot(int lotId);  
}  