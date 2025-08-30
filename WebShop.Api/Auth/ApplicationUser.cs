using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)] 
        public string? LastName { get; set; }

        [MaxLength(2048)]
        public string? ProfilePictureUrl { get; set; }

        [MaxLength(50)]
        public string? Country {  get; set; }
        [MaxLength(50)]
        public string? ShippingAdress1 { get; set; }
        [MaxLength(50)]
        public string? ShippingAdress2 { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        [MaxLength(50)]
        public string? PostalCode { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreateAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }


    }
}
