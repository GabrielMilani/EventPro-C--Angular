using Dapper;
using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.EventContext.Entities;
using System.Data;

namespace EventPro.Infrastructure.Context.EventContext.Repositories;

public class EventDapperRepository : IEventDapperRepository
{
    private readonly IDbConnection _dbConnection;

    public EventDapperRepository(IDbConnection dbConnection)
        => _dbConnection = dbConnection;

    public async Task<IEnumerable<Event>> GetEvents()
    {
        string query = "SELECT * FROM Events";
        return await _dbConnection.QueryAsync<Event>(query);
    }

    public async Task<Event?> GetEventById(int eventId)
    {
        string query = "SELECT * FROM Events WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Event>(query, new { Id = eventId });
    }
}