using System.ComponentModel.DataAnnotations;

namespace Inlämning2.Domain.Entities
{
    public class OrderProduct
    {
        [Key]
        public int OrderID { get; set; }
        [Key]
        public int ProductID { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
