using WebShop.Api.Auth.DTOs;

namespace WebShop.Api.Auth.Services.AuthService
{
    public interface IAuthService
    {
        Task<RegisterResult> RegisterAsync(RegisterRequest registerReq);
        Task<LoginResult> LoginAsync(LoginRequest loginReq);
    }
}
