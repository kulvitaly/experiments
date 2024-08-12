using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database
{
    public class RealEstateContext : DbContext
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties => Set<Property>();
        public DbSet<Payment> Payments => Set<Payment>();
    }
}
