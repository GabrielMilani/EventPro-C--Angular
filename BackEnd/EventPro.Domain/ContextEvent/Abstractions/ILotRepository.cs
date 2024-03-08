using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface ILotRepository
{
    Task<Lot[]> GetLotsByEventId(int eventId);
    Task<Lot> GetLotByIds(int eventId, int id);
    Task<Lot> DeleteLotByIds(int eventId, int id);

    Task<Lot> AddLot(Lot lot);
    Task<Lot> UpdateLot(Lot lot);
}  