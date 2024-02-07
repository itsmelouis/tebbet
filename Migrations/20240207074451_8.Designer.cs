﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tebbet.Database;

#nullable disable

namespace Tebbet.Migrations
{
    [DbContext(typeof(DatabaseConnection))]
    [Migration("20240207074451_8")]
    partial class _8
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tebbet.Models.Bets", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnOrder(1);

                    b.Property<int>("RaceId")
                        .HasColumnType("integer")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("Date_Bet")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Gains")
                        .HasColumnType("double precision");

                    b.Property<bool>("Has_Won")
                        .HasColumnType("boolean");

                    b.Property<double>("Odds")
                        .HasColumnType("double precision");

                    b.Property<int>("SnailId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RaceId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("Tebbet.Models.Circuits", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("id");

                    b.ToTable("Circuits");
                });

            modelBuilder.Entity("Tebbet.Models.Races", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("CircuitId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsEnded")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("CircuitId");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("Tebbet.Models.SnailParticipatingRace", b =>
                {
                    b.Property<int>("RacesId")
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    b.Property<int>("SnailsId")
                        .HasColumnType("integer")
                        .HasColumnOrder(2);

                    b.Property<int>("Ranking")
                        .HasColumnType("integer");

                    b.HasKey("RacesId", "SnailsId");

                    b.ToTable("SnailParticipatingRace");
                });

            modelBuilder.Entity("Tebbet.Models.Snails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("general_rank")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("id");

                    b.ToTable("Snails");
                });

            modelBuilder.Entity("Tebbet.Models.Users", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("Credits")
                        .HasColumnType("double precision");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Postalcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tebbet.Models.Races", b =>
                {
                    b.HasOne("Tebbet.Models.Circuits", "Circuits")
                        .WithMany("Races")
                        .HasForeignKey("CircuitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Circuits");
                });

            modelBuilder.Entity("Tebbet.Models.Circuits", b =>
                {
                    b.Navigation("Races");
                });
#pragma warning restore 612, 618
        }
    }
}
