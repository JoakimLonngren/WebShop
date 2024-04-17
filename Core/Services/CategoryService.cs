using Inlämning2.Core.Interfaces;
using Inlämning2.Data.Interfaces;
using Inlämning2.Domain.Entities;

namespace Inlämning2.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _repo.GetAllCategories();
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _repo.GetCategory(id);
            if(category == null)
            {
                throw new Exception("The category was not found.");
            }
            else
            {
                return await _repo.GetCategory(id);
            }
            
        }
    }
}
