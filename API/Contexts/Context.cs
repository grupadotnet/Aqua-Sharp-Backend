﻿using Aqua_Sharp_Backend.Comparers;
using Aqua_Sharp_Backend.Converters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Aqua_Sharp_Backend.Contexts
{
    public class Context : DbContext
    {
        #region Entities
        
        public DbSet<Aquarium> Aquarium { get; set; }
        public DbSet<Auth> Auth { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Measurement> Measurements{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        #endregion
        
        public Context(DbContextOptions<Context> options): base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateConfig(modelBuilder);
            CreateAquarium(modelBuilder);
            CreateDevices(modelBuilder);
            CreateMeasurement(modelBuilder);
        }

        private static void CreateConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auth>()
                .HasData(new Auth
                {
                    AuthId = 1,
                    Password = "AQAAAAIAAYagAAAAEL4Pun26YTba5pDt4Fc+EwYhVYl9wcF+0+5g7sNCk7O2f3gy1+4ByFs6HCs/sZXatQ==",
                    FirstRun = true,
                    Question = "",
                    Answer = ""

                });

            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id= 1,
                    Name = "all"

                });

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    UserId = 1,
                    Login = "Admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    AuthId = 1,
                    RoleId = 1

                }) ;
        }

        private static void CreateAquarium(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aquarium>(builder =>
            {
                builder.Property(x => x.Dawn)
                    .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
                builder.Property(x => x.Sunset)
                    .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
            });
            modelBuilder.Entity<Aquarium>().HasOne(a => a.Device)
                .WithOne(d => d.Aquarium)
                .HasForeignKey<Device>(d => d.AquariumId);

        }
        
        private static void CreateDevices(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasOne<Aquarium>(d => d.Aquarium)
                .WithOne(a => a.Device)
                .HasForeignKey<Device>(d => d.AquariumId);
        }
        
        private static void CreateMeasurement(ModelBuilder modelBuilder)
        {
        }
    }
}
