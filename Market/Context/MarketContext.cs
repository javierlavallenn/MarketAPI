using Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Context
{
    public class MarketContext(DbContextOptions<MarketContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Product

            modelBuilder.Entity<Product>()
                        .Property(p => p.Name)
                        .IsRequired();

            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(250);
            
            #endregion

            #region Category
            #endregion
        }
    }
}
