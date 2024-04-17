using System.ComponentModel.DataAnnotations;

namespace Inlämning2.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        //public List<OrderProduct> OrderProducts { get; set; }
        public int UserID { get; set; }
        public int TotalPrice { get; set; }


        //För att ha tillgång till flera produkter och kategorier.
        public virtual User? User { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
