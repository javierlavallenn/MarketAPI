using Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Context
{
    public class MarketContext(DbContextOptions<MarketContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        //    protected override void OnModelCreating(ModelBuilder modelBuilder)
        //    {
        //        #region Product
        //        modelBuilder.Entity<Product>()
        //            .Property(p => p.Name)
        //            .IsRequired()
        //            .HasMaxLength(150);


        //    modelBuilder.Entity<Product>()
        //            .Property(p => p.Brand)
        //            .IsRequired()
        //            .HasMaxLength(150);

        //    modelBuilder.Entity<Product>()
        //            .Property(p => p.Category)
        //            .IsRequired()
        //            .HasMaxLength(100);

        //    modelBuilder.Entity<Product>()
        //            .Property(p => p.BarCode)
        //            .HasMaxLength(50);

        //    modelBuilder.Entity<Product>()
        //            .Property(p => p.Description)
        //            .HasMaxLength(250);

        //    modelBuilder.Entity<Product>()
        //            .Property(p => p.Price)
        //            .IsRequired();

        //    modelBuilder.Entity<Product>()
        //            .Property(p => p.Stock)
        //            .IsRequired();

        //    modelBuilder.Entity<Product>()
        //             .HasOne<Category>()
        //        .WithMany(e => e.Products)
        //        .HasForeignKey(e => e.CategoryId)
        //        .IsRequired();
        //    #endregion


        //    #region Category
        //    modelBuilder.Entity<Category>().HasMany(e => e.Products)
        //        .WithOne(e => e.Category)
        //        .IsRequired();
        //    #endregion
        //}
    }
}
