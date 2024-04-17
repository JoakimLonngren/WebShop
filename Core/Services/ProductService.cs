using Inlämning2.Core.Interfaces;
using Inlämning2.Data.Interfaces;
using Inlämning2.Domain.DTOs;
using Inlämning2.Domain.Entities;

namespace Inlämning2.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ShowProductDTO>> GetAllProductDTOs()
        {
            List<Product> products = await _repo.GetAllProducts();
            List<ShowProductDTO> showProductDTOs = await MapProductsDTO(products);
            return showProductDTOs;

        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _repo.GetProduct(id);
            if(product != null)
            {
                return await _repo.GetProduct(id);
            }
            throw new Exception("No productID found.");
        }

        public async Task<List<ShowProductDTO>> MapProductsDTO(List<Product> products)
        {
            var productsDTOs = products.Select(p => new ShowProductDTO
            {
                ProductID = p.ProductID,
                Title = p.Title,
                Description = p.Description,
                Category = p.Category.Title,
                Price = p.Price,
            }).ToList();

            return await Task.FromResult(productsDTOs);
        }
    }
}
