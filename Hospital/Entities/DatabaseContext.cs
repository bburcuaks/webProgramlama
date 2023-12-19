using Microsoft.EntityFrameworkCore;

namespace Hospital.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options):base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Patient> Patients { get; set; }

       // public DbSet<Admin> Admins { get; set; }





    }
}
