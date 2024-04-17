using System.ComponentModel.DataAnnotations;

namespace Inlämning2.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
