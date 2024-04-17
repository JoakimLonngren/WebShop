using Inlämning2.Domain.Entities;

namespace Inlämning2.Domain.DTOs
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public List<ProductDTO> Products { get; set; }
        public int TotalPrice { get; set; }
    }
}
