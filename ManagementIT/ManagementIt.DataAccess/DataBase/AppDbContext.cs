using ManagementIt.Core.Domain.ApplicationEntity;
using Microsoft.EntityFrameworkCore;

namespace ManagementIt.DataAccess.DataBase
{
    public class AppDbContext : DbContext
    {
        public DbSet<ApplicationAction> ApplicationsAction { get; set; }
        public DbSet<ApplicationToIt> ApplicationsToIt { get; set; }
        public DbSet<ApplicationType> ApplicationsType { get; set; }
        public DbSet<ApplicationState> ApplicationsState { get; set; }
        public DbSet<ApplicationPriority> ApplicationsPriority { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationState>().HasData(
                new ApplicationState[]
                {
                    new ApplicationState {Id = 1, Name = "Свободная", IsDefault = true, BGColor = "LightYellow"},
                    new ApplicationState {Id = 2, Name = "Выполняется", IsDefault = false, BGColor = "LimeGreen"},
                    new ApplicationState {Id = 3, Name = "Выполнена", IsDefault = false, BGColor = "Salmon"}
                });
            modelBuilder.Entity<ApplicationPriority>().HasData(
                new ApplicationPriority[]
                {
                    new ApplicationPriority{Id = 1, Name = "Низкий", IsDefault = true },
                    new ApplicationPriority{Id = 2, Name = "Средний", IsDefault = false },
                    new ApplicationPriority{Id = 3, Name = "Высокий", IsDefault = false }
                });
        }
    }
}
