using Microsoft.AspNetCore.Identity;

namespace WebShop.Api.Auth.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken ct);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<SignInResult> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
    }
}
