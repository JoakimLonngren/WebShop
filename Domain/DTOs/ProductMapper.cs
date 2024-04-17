using Inlämning2.Domain.Entities;

namespace Inlämning2.Domain.DTOs
{

    //används ej
    public class ProductMapper
    {
        public static ShowProductDTO MapToProduct(Product product)
        {
            return new ShowProductDTO
            {
                ProductID = product.ProductID,
                Title = product.Title,
                Description = product.Description,
                Category = product.Category.Title,
                Price = product.Price
            };
        }
    }
}
