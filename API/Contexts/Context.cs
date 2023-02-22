using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Reflection;

namespace Aqua_Sharp_Backend.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Config>().HasNoKey();
        }

        #region Entities

        public DbSet<Config> Config { get; set; }
        public DbSet<Aquarium> Aquarium { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Measurement> Measurements{ get; set; }

        #endregion
    }
}
