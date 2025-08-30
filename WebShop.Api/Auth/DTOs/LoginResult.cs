namespace WebShop.Api.Auth.DTOs
{
    public record LoginResult(bool IsSuccess, string Message, AuthResponse? Auth = null);
}
