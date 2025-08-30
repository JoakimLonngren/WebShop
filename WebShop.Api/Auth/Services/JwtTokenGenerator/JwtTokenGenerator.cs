
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebShop.Api.Auth.Services.JwtTokenGenerator
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _cfg;

        public JwtTokenGenerator(IConfiguration cfg) => _cfg = cfg;
        public Task<(string token, DateTime expiresUtc)> GenerateAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var issuer = _cfg["Jwt:Issuer"]!;
            var audience = _cfg["Jwt:Audience"]!;
            var key = _cfg["Jwt:Key"]!;
            var minutes = int.TryParse(_cfg["Jwt:ExpiryMinutes"], out var m) ? m : 60;

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? user.Email ?? user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new("given_name", user.FirstName ?? string.Empty),
                new("lastname", user.LastName ?? string.Empty)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(minutes);

            var token = new JwtSecurityToken(issuer, audience, claims, DateTime.UtcNow, expires, creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult((jwt, expires));
            
        }
    }
}
