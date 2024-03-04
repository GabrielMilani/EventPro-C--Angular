using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.EventContext.Entities;
using System.Data;
using Dapper;

namespace EventPro.Infrastructure.Context.EventContext.Repositories;

public class LotDapperRepository  : ILotDapperRepository
{
    private readonly IDbConnection _dbConnection;

    public LotDapperRepository(IDbConnection dbConnection)
        => _dbConnection = dbConnection;

    public async Task<IEnumerable<Lot>> GetLots()
    {
        string query = "SELECT * FROM Lots";
        return await _dbConnection.QueryAsync<Lot>(query);
    }

    public async Task<Lot?> GetLotById(int lotId)
    {
        string query = "SELECT * FROM Lots WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Lot>(query, new { Id = lotId });
    }
}