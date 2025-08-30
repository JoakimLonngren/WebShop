using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Auth.DTOs
{
    public class RegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required, MinLength(6)]
        public string Password { get; set; } = default!;

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(2048)]
        public string? ProfilePictureUrl { get; set; }

        [MaxLength(50)]
        public string? Country { get; set; }
        [MaxLength(50)]
        public string? ShippingAdress1 { get; set; }
        [MaxLength(50)]
        public string? ShippingAdress2 { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        [MaxLength(50)]
        public string? PostalCode { get; set; }
    }
}
