using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebShop.Api.Categories.Domain;

namespace WebShop.Api.Products.Domain;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
}
