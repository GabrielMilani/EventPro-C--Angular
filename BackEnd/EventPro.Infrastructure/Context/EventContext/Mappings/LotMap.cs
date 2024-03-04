using EventPro.Domain.EventContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPro.Infrastructure.Context.EventContext.Mappings;

public class LotMap : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Price)
            .IsRequired();
        builder.Property(x => x.InitialDate)
            .IsRequired();
        builder.Property(x => x.FinalDate)
            .IsRequired();
        builder.Property(x => x.Quantity)
            .IsRequired();
    }
}