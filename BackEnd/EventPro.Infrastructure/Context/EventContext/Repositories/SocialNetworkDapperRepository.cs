using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.EventContext.Entities;
using System.Data;
using Dapper;

namespace EventPro.Infrastructure.Context.EventContext.Repositories;

public class SocialNetworkDapperRepository : ISocialNetworkDapperRepository
{
    private readonly IDbConnection _dbConnection;

    public SocialNetworkDapperRepository(IDbConnection dbConnection) 
        => _dbConnection = dbConnection;
    
    public async Task<IEnumerable<SocialNetwork>> GetSocialNetworks()
    {
        string query = "SELECT * FROM SocialNetworks";
        return await _dbConnection.QueryAsync<SocialNetwork>(query);
    }

    public async Task<SocialNetwork?> GetSocialNetworkById(int socialNetworkId)
    {
        string query = "SELECT * FROM SocialNetworks WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<SocialNetwork>(query, new { Id = socialNetworkId });
    }
}