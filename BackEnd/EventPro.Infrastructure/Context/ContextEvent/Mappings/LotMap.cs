using EventPro.Domain.ContextEvent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPro.Infrastructure.Context.ContextEvent.Mappings;

public class LotMap : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Price)
            .HasColumnType("NUMERIC")
            .HasPrecision(10, 2)
            .IsRequired();
        builder.Property(x => x.InitialDate)
            .IsRequired();
        builder.Property(x => x.FinalDate)
            .IsRequired();
        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.HasOne(e => e.Event)
            .WithMany(l => l.Lots)
            .OnDelete(DeleteBehavior.Cascade);
    }
}