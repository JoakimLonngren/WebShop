namespace WebShop.Api.Auth.DTOs
{
    public record RegisterResult(bool IsSuccess, string Message, string? UserId = null, AuthResponse? Auth = null);
}
