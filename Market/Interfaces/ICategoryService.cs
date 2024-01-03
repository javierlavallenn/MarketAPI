using Market.Entities;

namespace Market.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();

        Category GetById(Guid id);

        bool Create(Category category);

        bool Update(Category category);

        bool Delete(Category category);
    }
}
