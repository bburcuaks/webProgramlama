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
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {





            //doctor-appointment       
                   modelBuilder.Entity<Doctor>()
                 .HasMany(d => d.Appointments) //Bir doktorun birden çok randevusu olabilir.
                  .WithOne(a => a.Doctor)// Bir randevu yalnızca bir doktora ait olabilir.
                 .HasForeignKey(a => a.DoctorId);

            //doctor-departmant
            modelBuilder.Entity<Department>()  

           .HasMany(P => P.Doctors)   //Bir doktor yalnızca bir bölüme ait olabilir.
           .WithOne(d => d.Department)     //Bir doktor yalnızca bir bölüme ait olabilir.
           .HasForeignKey(d => d.DepartmentId);

         


            //user ile appointment ilişki
            modelBuilder.Entity<Appointment>()  
                .HasOne(a => a.User)//Bir randevu yalnızca bir kullanıcıya ait olabilir.
                .WithMany(u => u.Appointments)  //Bir kullanıcının birden çok randevusu olabilir.
                .HasForeignKey(a => a.Id);

        }
    }
    
    
}
