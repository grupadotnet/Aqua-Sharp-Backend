﻿// <auto-generated />
using System;
using Aqua_Sharp_Backend.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230428210900_updt")]
    partial class updt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.Entities.Aquarium", b =>
                {
                    b.Property<int>("AquariumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AquariumId"));

                    b.Property<TimeOnly>("Dawn")
                        .HasColumnType("time without time zone");

                    b.Property<long>("Height")
                        .HasColumnType("bigint");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("PH")
                        .HasColumnType("real");

                    b.Property<TimeOnly>("Sunset")
                        .HasColumnType("time without time zone");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<long>("Width")
                        .HasColumnType("bigint");

                    b.HasKey("AquariumId");

                    b.ToTable("Aquarium");
                });

            modelBuilder.Entity("Models.Entities.Config", b =>
                {
                    b.Property<int>("ConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ConfigId"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("FirstRun")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ConfigId");

                    b.ToTable("Config");

                    b.HasData(
                        new
                        {
                            ConfigId = 1,
                            Answer = "",
                            FirstRun = true,
                            Password = "password",
                            Question = ""
                        });
                });

            modelBuilder.Entity("Models.Entities.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DeviceId"));

                    b.Property<int>("AquariumId")
                        .HasColumnType("integer");

                    b.Property<bool>("ManualMode")
                        .HasColumnType("boolean");

                    b.Property<long>("MeasurementFrequency")
                        .HasColumnType("bigint");

                    b.HasKey("DeviceId");

                    b.HasIndex("AquariumId")
                        .IsUnique();

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Models.Entities.Measurement", b =>
                {
                    b.Property<int>("MeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MeasurementId"));

                    b.Property<int>("AquariumId")
                        .HasColumnType("integer");

                    b.Property<bool>("LightOn")
                        .HasColumnType("boolean");

                    b.Property<long>("TDS")
                        .HasColumnType("bigint");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("MeasurementId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Models.Entities.Device", b =>
                {
                    b.HasOne("Models.Entities.Aquarium", "Aquarium")
                        .WithOne("Device")
                        .HasForeignKey("Models.Entities.Device", "AquariumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aquarium");
                });

            modelBuilder.Entity("Models.Entities.Aquarium", b =>
                {
                    b.Navigation("Device")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
