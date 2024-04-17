using Inlämning2.Domain.Entities;

namespace Inlämning2.Data.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task <Category> GetCategory(int id);
    }
}
