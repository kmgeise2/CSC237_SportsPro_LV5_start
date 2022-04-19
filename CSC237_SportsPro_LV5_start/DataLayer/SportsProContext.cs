using Microsoft.EntityFrameworkCore;

namespace CSC237_SportsPro_LV5_start.Models
{
    public class SportsProContext : DbContext
    {
        public SportsProContext(DbContextOptions<SportsProContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure many-to-many for Registrations table
            modelBuilder.ApplyConfiguration(new RegistrationConfig());

            // seed initial data
            modelBuilder.ApplyConfiguration(new SeedProducts());
            modelBuilder.ApplyConfiguration(new SeedTechnicians());
            modelBuilder.ApplyConfiguration(new SeedCountries());
            modelBuilder.ApplyConfiguration(new SeedCustomers());
            modelBuilder.ApplyConfiguration(new SeedIncidents());
            modelBuilder.ApplyConfiguration(new SeedRegistrations());
        }
    }
}