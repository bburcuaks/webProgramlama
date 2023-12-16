using Microsoft.EntityFrameworkCore;

namespace Hospital.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options):base(options)
        { }
        public DbSet<User> Users { get; set; } 

    }
}
