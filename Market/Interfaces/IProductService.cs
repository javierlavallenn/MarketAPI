using Market.Entities;
using System.Linq.Expressions;

namespace Market.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(Expression<Func<Product, bool>> expression);

        Product GetById(Guid id);

        bool Create(Product product);

        bool Update(Product product);

        bool Delete(Product product);
    }
}
