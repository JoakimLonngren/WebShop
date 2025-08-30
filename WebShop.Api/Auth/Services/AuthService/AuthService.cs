using WebShop.Api.Auth.DTOs;
using WebShop.Api.Auth.Repositories;
using WebShop.Api.Auth.Services.JwtTokenGenerator;

namespace WebShop.Api.Auth.Services.AuthService
{
    public class AuthService(IAuthRepository repo, IJwtTokenGenerator jwt) : IAuthService
    {
        private readonly IAuthRepository _repo = repo;
        private readonly IJwtTokenGenerator _jwt = jwt;

        public async Task<LoginResult> LoginAsync(LoginRequest loginReq)
        {
            var user = await _repo.FindByEmailAsync(loginReq.Email);
            if (user == null)
                return new LoginResult(false, "User was not found.");

            var password = await _repo.CheckPasswordAsync(user, loginReq.Password);
            if (!password.Succeeded) return new LoginResult(false, "Password was not correct.");

            var roles = await _repo.GetRolesAsync(user);
            var (token, expires) = await _jwt.GenerateAsync(user, roles);

            return new LoginResult(
                IsSuccess: true,
                Message: "User logged in successfully.",
                Auth: new AuthResponse(token, expires)
            );
        }

        public async Task<RegisterResult> RegisterAsync(RegisterRequest registerReq)
        {
            var userExists = await _repo.FindByEmailAsync(registerReq.Email);
            if (userExists != null)
                return new RegisterResult(false, "Email is already in use.");

            var user = new ApplicationUser
            {
                UserName = registerReq.Email,
                Email = registerReq.Email,
                FirstName = registerReq.FirstName,
                LastName = registerReq.LastName,
                Country = registerReq.Country,
                ShippingAdress1 = registerReq.ShippingAdress1,
                ShippingAdress2 = registerReq.ShippingAdress2,
                City = registerReq.City,
                PostalCode = registerReq.PostalCode,
                EmailConfirmed = true
            };

            var result = await _repo.CreateUserAsync(user, registerReq.Password);
            if (!result.Succeeded)
                return new RegisterResult(false, string.Join("; ", result.Errors.Select(e => e.Description)));

            var roles = await _repo.GetRolesAsync(user);
            var (token, expires) = await _jwt.GenerateAsync(user, roles);

            return new RegisterResult(
                IsSuccess: true, 
                Message: "User created successfully.", 
                UserId: user.Id, 
                Auth: new AuthResponse(token, expires)
            );
        }
    }
}
