using System.Data;
using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextShared.Abstractions;
using EventPro.Infrastructure.Context.ContextEvent.Repositories;
using EventPro.Infrastructure.Context.ContextShared;
using EventPro.Infrastructure.Data;
using Microsoft.Data.SqlClient;
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

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ISpeakerRepository, SpeakerRepository>();
        services.AddScoped<ILotRepository, LotRepository>();
        services.AddScoped<ISocialNetworkRepository, SocialNetworkRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        var myHandlers = AppDomain.CurrentDomain.Load("EventPro.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myHandlers));

        return services;
    } 
}