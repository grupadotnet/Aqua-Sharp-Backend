﻿// <auto-generated />
using System;
using Aqua_Sharp_Backend.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Entities.Aquarium", b =>
                {
                    b.Property<int>("AquariumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AquariumId"));

                    b.Property<TimeSpan>("Dawn")
                        .HasColumnType("time");

                    b.Property<long>("Height")
                        .HasColumnType("bigint");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PH")
                        .HasColumnType("real");

                    b.Property<TimeSpan>("Sunset")
                        .HasColumnType("time");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<long>("Width")
                        .HasColumnType("bigint");

                    b.HasKey("AquariumId");

                    b.HasIndex("UserId");

                    b.ToTable("Aquarium");
                });

            modelBuilder.Entity("Models.Entities.Auth", b =>
                {
                    b.Property<int>("AuthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthId"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FirstRun")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthId");

                    b.ToTable("Auth");

                    b.HasData(
                        new
                        {
                            AuthId = 1,
                            Answer = "",
                            FirstRun = true,
                            Password = "AQAAAAIAAYagAAAAEO7X30v9YcG0ELCt0w9SY8g+1weZE6bir+SUjlux34k0eNJ55xDCzbEiKAyvM6rniw==",
                            Question = ""
                        },
                        new
                        {
                            AuthId = 2,
                            Answer = "",
                            FirstRun = true,
                            Password = "AQAAAAIAAYagAAAAEG2IulPNa9UbQC0NvKOpiaBd4ay8q9WXfsZZrDvSiM7Y3Rx0riVc7UHjH149n7YmPQ==",
                            Question = ""
                        });
                });

            modelBuilder.Entity("Models.Entities.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeviceId"));

                    b.Property<int>("AquariumId")
                        .HasColumnType("int");

                    b.Property<string>("AzureDeviceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ManualMode")
                        .HasColumnType("bit");

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
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MeasurementId"));

                    b.Property<int>("AquariumId")
                        .HasColumnType("int");

                    b.Property<bool>("LightOn")
                        .HasColumnType("bit");

                    b.Property<float>("Ph")
                        .HasColumnType("real");

                    b.Property<long>("TDS")
                        .HasColumnType("bigint");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("MeasurementId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Models.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = 8
                        },
                        new
                        {
                            Id = 2,
                            Name = 1
                        });
                });

            modelBuilder.Entity("Models.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("AuthId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("AuthId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            AuthId = 1,
                            FirstName = "Admin",
                            LastName = "Admin",
                            Login = "Admin",
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            AuthId = 2,
                            FirstName = "Jan",
                            LastName = "Kowalski",
                            Login = "User",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("Models.Entities.Aquarium", b =>
                {
                    b.HasOne("Models.Entities.User", "User")
                        .WithMany("Aquariums")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("Models.Entities.User", b =>
                {
                    b.HasOne("Models.Entities.Auth", "Auth")
                        .WithOne("User")
                        .HasForeignKey("Models.Entities.User", "AuthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auth");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Models.Entities.Aquarium", b =>
                {
                    b.Navigation("Device")
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Entities.Auth", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Entities.User", b =>
                {
                    b.Navigation("Aquariums");
                });
#pragma warning restore 612, 618
        }
    }
}
