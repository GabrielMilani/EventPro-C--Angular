﻿using System.Security.Cryptography.X509Certificates;
using EventPro.Domain.ContextEvent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPro.Infrastructure.Context.ContextEvent.Mappings;

public class EventMap : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Theme)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Local)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Email)
            .HasMaxLength(150)
            .IsRequired();
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(x => x.QuantityPeople)
            .IsRequired();
        builder.Property(x => x.EventDate)
            .IsRequired();
        


    }
}