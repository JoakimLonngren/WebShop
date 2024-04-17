using Inlämning2.Domain.DTOs;
using Inlämning2.Domain.Entities;

namespace Inlämning2.Core.Interfaces
{
    public interface IProductService
    {
        Task<List<ShowProductDTO>> GetAllProductDTOs();
        Task<List<ShowProductDTO>> MapProductsDTO(List<Product> products);
        Task<Product> GetProduct(int id);
    }
}
