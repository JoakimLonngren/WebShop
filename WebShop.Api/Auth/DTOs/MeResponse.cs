namespace WebShop.Api.Auth.DTOs
{
    public record MeResponse(string Id, string Email, string[] Roles);
}
