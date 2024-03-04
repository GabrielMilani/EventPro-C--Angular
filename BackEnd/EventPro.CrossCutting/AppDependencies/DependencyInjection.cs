using System.Data;
using EventPro.Domain.EventContext.Abstractions;
using EventPro.Domain.SharedContext.Abstractions;
using EventPro.Infrastructure.Context.EventContext.Repositories;
using EventPro.Infrastructure.Context.SharedContext;
using EventPro.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventPro.CrossCutting.AppDependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            return connection;
        });   
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventDapperRepository, EventDapperRepository>();        
        services.AddScoped<ISpeakerRepository, SpeakerRepository>();
        services.AddScoped<ISpeakerDapperRepository, SpeakerDapperRepository>();        
        services.AddScoped<ILotRepository, LotRepository>();
        services.AddScoped<ILotDapperRepository, LotDapperRepository>();        
        services.AddScoped<ISocialNetworkRepository, SocialNetworkRepository>();
        services.AddScoped<ISocialNetworkDapperRepository, SocialNetworkDapperRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        var myHandlers = AppDomain.CurrentDomain.Load("EventPro.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myHandlers));

        return services;
    }
}