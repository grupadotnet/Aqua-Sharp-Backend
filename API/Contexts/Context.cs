using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Aqua_Sharp_Backend.Contexts
{
    public class Context : DbContext
    {
        #region Entities

        public DbSet<Config> Config { get; set; }
        public DbSet<Aquarium> Aquarium { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Measurement> Measurements{ get; set; }

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
            modelBuilder.Entity<Config>()
                .HasData(new Config
                {
                    Id = 1,
                    Password = "password",
                    FirstRun = true,
                    Question = "",
                    Answer = ""
                });
        }

        private static void CreateAquarium(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aquarium>()
                .HasOne(a => a.Device)
                .WithOne(d => d.Aquarium);
                
        }
        
        private static void CreateDevices(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasOne(a => a.Aquarium)
                .WithOne(d => d.Device)
                .HasForeignKey<Aquarium>(a => a.DeviceId);;
        }
        
        private static void CreateMeasurement(ModelBuilder modelBuilder)
        {
            
        }
    }
}
