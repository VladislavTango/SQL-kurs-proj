using Microsoft.EntityFrameworkCore;
using Testovoe.Models;

namespace Testovoe.Infrastructure.AppContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DoctorModel> Doctors { get; set; } = null!;
        public DbSet<DoctorsRoomModel> DoctorsRooms { get; set; } = null!;
        public DbSet<PatientModel> Patients { get; set; } = null!;
        public DbSet<RegionModel> Regions { get; set; } = null!;
        public DbSet<SpecializationModel> Specializations { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        }
    }
}
