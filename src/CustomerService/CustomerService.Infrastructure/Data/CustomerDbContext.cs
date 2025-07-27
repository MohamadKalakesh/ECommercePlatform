using CustomerService.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(cfg =>
            {
                cfg.HasKey(c => c.Id);
                cfg.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
                cfg.Property(c => c.LastName).IsRequired().HasMaxLength(50);
                cfg.Property(c => c.Email).IsRequired().HasMaxLength(200);
            });
        }
    }
}
