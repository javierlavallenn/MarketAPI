using Market.Context;
using Market.Entities;
using Market.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Market.Services
{
    public class ProductService(MarketContext context) : IProductService
    {
        private readonly MarketContext _context = context;

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(Guid id)
        {
            return _context.Products.Where(x => x.Id == id).FirstOrDefault()!;
        }

        public bool Create(Product product)
        {
            EntityEntry result = _context.Products.Add(product);

            if (result.State != EntityState.Added)
            {
                return false;
            }

            SaveChanges();

            return true;
        }

        public bool Update(Product product)
        {
            EntityEntry result = _context.Products.Update(product);

            if (result.State != EntityState.Modified)
            {
                return false;
            }

            SaveChanges();

            return true;
        }

        public bool Delete(Product product)
        {
            EntityEntry result = _context.Products.Remove(product);

            if (result.State != EntityState.Deleted)
            {
                return false;
            }

            SaveChanges();

            return true;
        }

        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> expression)
        {
            return _context.Products.AsNoTracking().Where(expression).ToList();
        }

        private void SaveChanges() => _context.SaveChanges();
    }
}
