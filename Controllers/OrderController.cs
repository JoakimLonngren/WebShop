using Inlämning2.Core.Interfaces;
using Inlämning2.Domain.DTOs;
using Inlämning2.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Inlämning2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [Route("Create")]
        [HttpPost]
        [Authorize]
        public async Task <IActionResult> CreateOrder(List<OrderProductDTO> orderProductDTO)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _service.Create(orderProductDTO, userID);
                return Ok("Order created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Orders")]
        [HttpGet]
        [Authorize]
        public async Task <IActionResult> ShowOrders()
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //return Unauthorized("You have to log in to be able to see your orders.");
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                List<OrderDTO> orders = await _service.GetAllDtoOrders(userID);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
