using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Aqua_Sharp_Backend.Contexts
{
    public class Context : DbContext
    {
        #region Entities
        
        public DbSet<Aquarium> Aquarium { get; set; }
        public DbSet<Config> Config { get; set; }
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
            modelBuilder.Entity<Config>(ef =>
            {
                ef.Property(c => c.ConfigId).UseSerialColumn().IsRequired();
                ef.HasKey(c => c.ConfigId);
                ef.HasIndex(c => c.ConfigId);
            });
            
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
            modelBuilder.Entity<Aquarium>(ef =>
            {
                ef.Property(a => a.AquariumId).UseSerialColumn().IsRequired();
                ef.HasKey(a => a.AquariumId);
                ef.HasIndex(a => a.AquariumId);
            });
            
            modelBuilder.Entity<Aquarium>()
                .HasOne(a => a.Device)
                .WithOne(d => d.Aquarium)
                .HasForeignKey<Device>(d => d.AquariumId);
        }
        
        private static void CreateDevices(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(ef =>
            {
                ef.Property(d => d.DeviceId).UseSerialColumn().IsRequired();
                ef.HasKey(d => d.DeviceId);
                ef.HasIndex(d => d.DeviceId);
            });
            
            modelBuilder.Entity<Device>()
                .HasOne(a => a.Aquarium)
                .WithOne(d => d.Device)
                .HasForeignKey<Aquarium>(a => a.DeviceId);
        }
        
        private static void CreateMeasurement(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measurement>(ef =>
            {
                ef.Property(m => m.MeasurementId).UseSerialColumn().IsRequired();
                ef.HasKey(m => m.MeasurementId);
                ef.HasIndex(m => m.MeasurementId);
            });
        }
    }
}
