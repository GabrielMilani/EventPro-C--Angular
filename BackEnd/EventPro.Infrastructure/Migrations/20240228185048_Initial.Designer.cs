﻿// <auto-generated />
using System;
using EventPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventPro.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240228185048_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantityPeople")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.Lot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FinalDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.SocialNetwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("SpeakerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SocialNetworks");
                });

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("MiniCV")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.SpeakerEvent", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpeakerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SpeakerEvents");
                });

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.Lot", b =>
                {
                    b.HasOne("EventPro.Domain.EventContext.Entities.Event", "Event")
                        .WithMany("Lots")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.SocialNetwork", b =>
                {
                    b.HasOne("EventPro.Domain.EventContext.Entities.Event", "Event")
                        .WithMany("SocialNetworks")
                        .HasForeignKey("EventId");

                    b.HasOne("EventPro.Domain.EventContext.Entities.Speaker", "Speaker")
                        .WithMany("SocialNetworks")
                        .HasForeignKey("SpeakerId");

                    b.Navigation("Event");

                    b.Navigation("Speaker");
                });

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.SpeakerEvent", b =>
                {
                    b.HasOne("EventPro.Domain.EventContext.Entities.Event", "Event")
                        .WithMany("SpeakerEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventPro.Domain.EventContext.Entities.Speaker", "Speaker")
                        .WithMany("SpeakerEvents")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Speaker");
                });

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.Event", b =>
                {
                    b.Navigation("Lots");

                    b.Navigation("SocialNetworks");

                    b.Navigation("SpeakerEvents");
                });

            modelBuilder.Entity("EventPro.Domain.EventContext.Entities.Speaker", b =>
                {
                    b.Navigation("SocialNetworks");

                    b.Navigation("SpeakerEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
