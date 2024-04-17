using Inlämning2.Domain.Entities;

namespace Inlämning2.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategory(int id);
    }
}
