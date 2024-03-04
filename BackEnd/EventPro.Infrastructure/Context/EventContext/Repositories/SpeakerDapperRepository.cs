using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.EventContext.Entities;
using System.Data;
using Dapper;

namespace EventPro.Infrastructure.Context.EventContext.Repositories;

public class SpeakerDapperRepository : ISpeakerDapperRepository
{
    private readonly IDbConnection _dbConnection;

    public SpeakerDapperRepository(IDbConnection dbConnection)
        => _dbConnection = dbConnection;

    public async Task<IEnumerable<Speaker>> GetSpeakers()
    {
        string query = "SELECT * FROM Speakers";
        return await _dbConnection.QueryAsync<Speaker>(query);
    }

    public async Task<Speaker?> GetSpeakerById(int speakerId)
    {
        string query = "SELECT * FROM Speakers WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Speaker>(query, new { Id = speakerId });
    }
}