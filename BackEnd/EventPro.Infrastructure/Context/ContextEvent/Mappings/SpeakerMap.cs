using EventPro.Domain.ContextEvent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPro.Infrastructure.Context.ContextEvent.Mappings;

public class SpeakerMap : IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Telephone)
            .IsRequired();
        builder.Property(x => x.Email)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasMany(x => x.SocialNetworks)
            .WithOne(s => s.Speaker)
            .OnDelete(DeleteBehavior.Cascade);

    }
}