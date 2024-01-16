using Market.Model.Entities;

namespace Market.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();

        Task<Category> GetById(Guid id);

        Task<bool> Create(Category category);

        Task<bool> Delete(Category category);
    }
}
