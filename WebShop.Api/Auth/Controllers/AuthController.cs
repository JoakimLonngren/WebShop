using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.Api.Auth.DTOs;
using WebShop.Api.Auth.Services.AuthService;

namespace WebShop.Api.Auth.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest registerReq)
        {
            var result = await _authService.RegisterAsync(registerReq);
            if (!result.IsSuccess)
                return BadRequest(new { result.Message });

            return Ok(result.Auth!);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest loginReq)
        {
            var auth = await _authService.LoginAsync(loginReq);
            if (auth == null)
                return Unauthorized(new { message = "Invalid login credentials." });

            return Ok(auth);
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<MeResponse> Me()
        {
            return Ok(new
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub")?.Value,
                Email = User.FindFirstValue(ClaimTypes.Email),
                Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray()
            });
        }
    }
}
