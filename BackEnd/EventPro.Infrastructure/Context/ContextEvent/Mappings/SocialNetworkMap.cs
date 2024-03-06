using EventPro.Domain.ContextEvent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPro.Infrastructure.Context.ContextEvent.Mappings;

public class SocialNetworkMap : IEntityTypeConfiguration<SocialNetwork>
{
    public void Configure(EntityTypeBuilder<SocialNetwork> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.URL)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(s => s.Speaker)
            .WithMany(sn => sn.SocialNetworks)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Event)
            .WithMany(sn => sn.SocialNetworks)
            .OnDelete(DeleteBehavior.Cascade);
    }
}