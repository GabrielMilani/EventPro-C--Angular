using EventPro.Domain.ContextEvent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPro.Infrastructure.Context.ContextEvent.Mappings;

public class SpeakerEventMap : IEntityTypeConfiguration<SpeakerEvent>
{
    public void Configure(EntityTypeBuilder<SpeakerEvent> builder)
    {
        builder.HasKey(x => new { x.EventId, x.SpeakerId });
    }
}