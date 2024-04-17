using Inlämning2.Domain.Entities;

namespace Inlämning2.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task Create(Order order);
        Task<Product> GetProduct(int id);
    }
}
