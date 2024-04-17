using System.ComponentModel.DataAnnotations;

namespace Inlämning2.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

    }
}
