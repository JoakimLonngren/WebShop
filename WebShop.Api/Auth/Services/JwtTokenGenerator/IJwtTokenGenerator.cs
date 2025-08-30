namespace WebShop.Api.Auth.Services.JwtTokenGenerator
{
    public interface IJwtTokenGenerator
    {
        Task<(string token, DateTime expiresUtc)> GenerateAsync(ApplicationUser user, IEnumerable<string> roles);
    }
}
