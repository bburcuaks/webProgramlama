using Microsoft.EntityFrameworkCore;

namespace Hospital.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options):base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Appointment> Doctors { get; set; }
        public DbSet<Appointment> Departments { get; set; }
        public DbSet<Appointment> Patients { get; set; }





    }
}
