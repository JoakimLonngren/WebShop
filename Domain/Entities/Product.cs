using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inlämning2.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public int Price { get; set; }

        
        public virtual Category Category { get; set; }
        //public virtual Order Order { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set;}
    }
}
