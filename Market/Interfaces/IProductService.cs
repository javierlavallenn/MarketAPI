using Market.Model.Entities;

namespace Market.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();

        Task<Product> GetById(Guid id);

        Task<bool> Create(Product product);

        Task<bool> Update(Product product);

        Task<bool> Delete(Product product);
    }
}
