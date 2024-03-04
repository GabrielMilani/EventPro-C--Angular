using EventPro.Domain.EventContext.Entities;
using EventPro.Infrastructure.Context.EventContext.Mappings;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Lot> Lots { get; set; }    
    public DbSet<SocialNetwork> SocialNetworks { get; set; }
    public DbSet<SpeakerEvent> SpeakerEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EventMap());
        builder.ApplyConfiguration(new SpeakerMap());
        builder.ApplyConfiguration(new LotMap());
        builder.ApplyConfiguration(new SocialNetworkMap());
        builder.ApplyConfiguration(new SpeakerEventMap());
    }

}