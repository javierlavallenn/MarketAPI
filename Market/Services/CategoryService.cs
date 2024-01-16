using Market.Context;
using Market.Interfaces;
using Market.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Market.Services
{
    public class CategoryService(MarketContext context) : ICategoryService
    {
        private readonly MarketContext _context = context;

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.Include(p => p.Products).ToListAsync();
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _context.Categories.Include(p => p.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Create(Category category)
        {
            EntityEntry result = await _context.Categories.AddAsync(category);

            if (result.State != EntityState.Added)
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Category category)
        {
            EntityEntry result = _context.Categories.Remove(category);

            if (result.State != EntityState.Deleted)
            {
                return false;
            }
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
