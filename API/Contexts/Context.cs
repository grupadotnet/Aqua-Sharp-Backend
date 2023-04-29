using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Aqua_Sharp_Backend.Contexts
{
    public class Context : DbContext
    {
        #region Entities
        
        public virtual DbSet<Aquarium> Aquarium { get; set; }
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Measurement> Measurements{ get; set; }

        #endregion
        
        public Context() { }
        
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
                    ConfigId = 1,
                    Password = "password",
                    FirstRun = true,
                    Question = "",
                    Answer = ""
                });
        }

        private static void CreateAquarium(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aquarium>()
                .HasOne<Device>(a => a.Device)
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
