using AppointmentServiceAPI.Entities;
using Microsoft.EntityFrameworkCore;
//using PatientServiceAPI.Entities;
//using DoctorServiceAPI.Entities;


namespace AppointmentServiceAPI.Data
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        //public DbSet<Patient> Patients { get; set; }

        //public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.PatientId).IsRequired();
                entity.Property(a => a.DoctorId).IsRequired();
                entity.Property(a => a.AppointmentDate).IsRequired();
                entity.Property(a => a.Status).HasConversion<int>()
                .IsRequired();
            });
        }
    }
}
