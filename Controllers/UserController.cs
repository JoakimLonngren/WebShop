using Inlämning2.Core.Interfaces;
using Inlämning2.Domain.DTOs;
using Inlämning2.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inlämning2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Route("Create")]
        [HttpPost]
        public async Task <IActionResult> Create(User user)
        {
            try
            {
                await _service.Create(user);
                return Ok("User created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("LogIn")]
        [HttpPost]
        
        public async Task <IActionResult> LogIn(UserDTO userDTO)
        {
            try
            {
                var token = await _service.LogIn(userDTO);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Route("GetThis")]
        [HttpGet]
        [Authorize]
        public async Task <IActionResult> GetThis()
        {
            return Ok("Blablabla");
        }
    }
}
