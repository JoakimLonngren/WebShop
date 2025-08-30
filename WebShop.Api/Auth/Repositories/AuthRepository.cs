using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebShop.Api.Auth.Repositories
{
    public class AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signIn) : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signIn = signIn;

        public async Task<SignInResult> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _signIn.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email, ct);
        }

        public async Task<ApplicationUser?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
