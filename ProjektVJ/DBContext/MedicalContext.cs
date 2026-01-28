using Microsoft.EntityFrameworkCore;
using Medical.Models;

namespace Medical.DBContext
{
    public class MedicalContext : DbContext
    {
        public MedicalContext(DbContextOptions<MedicalContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();
        public DbSet<Drug> Drugs => Set<Drug>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<Examination> Examinations => Set<Examination>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.OIB)
                .IsUnique();

            modelBuilder.Entity<Patient>()
                .Property(p => p.BirthDate)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Examination>()
                .HasOne(e => e.SpecialistDoctor)
                .WithMany()
                .HasForeignKey(e => e.SpecialistDoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    FirstName = "Ivan",
                    LastName = "Ivić",
                    Specialization = "Kardiolog"
                },
                new Doctor
                {
                    Id = 2,
                    FirstName = "Ana",
                    LastName = "Anić",
                    Specialization = "Neurolog"
                }
            );
        }
    }
}
