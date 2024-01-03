using Market.Context;
using Market.Entities;
using Market.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Market.Services
{
    public class CategoryService(MarketContext context) : ICategoryService
    {
        private readonly MarketContext _context = context;

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(Guid id)
        {
            return _context.Categories.Where(x => x.Id == id).FirstOrDefault()!;
        }

        public bool Create(Category category)
        {
            EntityEntry result = _context.Categories.Add(category);

            if (result.State != EntityState.Added)
            {
                return false;
            }

            _context.SaveChanges();

            return true;
        }

        public bool Update(Category category)
        {
            EntityEntry result = _context.Categories.Update(category);

            if (result.State != EntityState.Modified)
            {
                return false;
            }

            _context.SaveChanges();

            return true;
        }

        public bool Delete(Category category)
        {
            EntityEntry result = _context.Categories.Remove(category);

            if (result.State != EntityState.Deleted)
            {
                return false;
            }

            _context.SaveChanges();

            return true;
        }
    }
}
