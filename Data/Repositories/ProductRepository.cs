using Inlämning2.Data.Context;
using Inlämning2.Data.Interfaces;
using Inlämning2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inlämning2.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebShopContext _context;

        public ProductRepository(WebShopContext context)
        {
            _context = context;
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.Include(p => p.OrderProducts)
                                          .Include(p => p.Category)
                                          .FirstOrDefaultAsync(p => p.ProductID == id);
        }
    }
}
