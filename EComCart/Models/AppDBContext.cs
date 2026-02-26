using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EComCart.Models
{
    public class AppDbContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                "Data Source = MSI\\HSQL;Initial Catalog = EComCart; Integrated Security = True; Encrypt=False;Trust Server Certificate=True"
            );
        }

        // Customer table (only needed for customer flow)
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}