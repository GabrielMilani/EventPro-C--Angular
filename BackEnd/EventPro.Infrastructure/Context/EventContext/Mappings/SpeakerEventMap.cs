using EventPro.Domain.EventContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPro.Infrastructure.Context.EventContext.Mappings;

public class SpeakerEventMap : IEntityTypeConfiguration<SpeakerEvent>
{
    public void Configure(EntityTypeBuilder<SpeakerEvent> builder)
    {
        builder.HasKey(x => new { x.EventId, x.SpeakerId });
    }
}