using Inlämning2.Data.Context;
using Inlämning2.Data.Interfaces;
using Inlämning2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inlämning2.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebShopContext _context;

        public CategoryRepository(WebShopContext context)
        {
            _context = context;
        }



        //Asynkront anrop för att ta hem alla kategorier.
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);
        }

    }
}
