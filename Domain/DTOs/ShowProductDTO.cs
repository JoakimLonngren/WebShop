using Inlämning2.Domain.Entities;

namespace Inlämning2.Domain.DTOs
{
    public class ShowProductDTO
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int? Quantity { get; set; }
    }
}
