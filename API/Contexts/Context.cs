using Aqua_Sharp_Backend.Authorization;
using Aqua_Sharp_Backend.Comparers;
using Aqua_Sharp_Backend.Converters;
using Microsoft.AspNetCore.Identity;
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
            var Adm = new Auth
            {
                AuthId = 1,
                FirstRun = true,
                Question = "",
                Answer = ""
            };

            var admConfig = AdminConfig.Configuration.GetSection("Authentication:AdminPassword").Value;
            Adm.Password = new PasswordHasher<Auth>().HashPassword(Adm, admConfig);



            modelBuilder.Entity<Auth>()
                .HasData(Adm);


            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = 1,
                    Name = RoleName.All

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



            var Usr = new Auth
            {
                AuthId = 2,
                FirstRun = true,
                Question = "",
                Answer = ""
            };

            var usrConfig = AdminConfig.Configuration.GetSection("Authentication:UserPassword").Value;
            Usr.Password = new PasswordHasher<Auth>().HashPassword(Usr, usrConfig);



            modelBuilder.Entity<Auth>()
                .HasData(Usr);


            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = 2,
                    Name = RoleName.Own

                });

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    UserId = 2,
                    Login = "User",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    AuthId = 2,
                    RoleId = 2

                });




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
