using System.ComponentModel.DataAnnotations;
using WebShop.Api.Products.Domain;

namespace WebShop.Api.Categories.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
