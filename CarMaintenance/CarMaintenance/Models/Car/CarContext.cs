using CarMaintenance.Models.User;

using Microsoft.EntityFrameworkCore;


namespace CarMaintenance.Models.Car
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {
        }

        public DbSet<CarDetails> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarDetails>().ToTable("Cars");
        }
    }
}
