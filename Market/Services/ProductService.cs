using Market.Context;
using Market.Interfaces;
using Market.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Market.Services
{
    public class ProductService(MarketContext context) : IProductService
    {
        private readonly MarketContext _context = context;

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.Include(c => c.Category).ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> Create(Product product)
        {
            EntityEntry result = await _context.Products.AddAsync(product);

            if (result.State != EntityState.Added)
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(Product product)
        {
            EntityEntry result = _context.Products.Update(product);

            if (result.State != EntityState.Modified)
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Product product)
        {
            EntityEntry result = _context.Products.Remove(product);

            if (result.State != EntityState.Deleted)
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
