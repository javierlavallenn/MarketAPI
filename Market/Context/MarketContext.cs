using Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Context
{
    public class MarketContext(DbContextOptions<MarketContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
