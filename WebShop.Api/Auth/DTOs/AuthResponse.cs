namespace WebShop.Api.Auth.DTOs
{
    public record AuthResponse(string AccessToken, DateTime ExpiresAtUtc);
}
