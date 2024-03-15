using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextEvent.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User, Role, int, 
                            IdentityUserClaim<int>, UserRole, 
                            IdentityUserLogin<int>, IdentityRoleClaim<int>,IdentityUserToken<int>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Event> Events { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Lot> Lots { get; set; }    
    public DbSet<SocialNetwork> SocialNetworks { get; set; }
    public DbSet<SpeakerEvent> SpeakerEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.UserId)
                .IsRequired();
        });

        builder.Entity<SpeakerEvent>()
            .HasKey(PE => new {PE.EventId, PE.SpeakerId});

        builder.Entity<Event>()
            .HasMany(e => e.SocialNetworks)
            .WithOne(rs => rs.Event)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Speaker>()
            .HasMany(e => e.SocialNetworks)
            .WithOne(rs => rs.Speaker)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
