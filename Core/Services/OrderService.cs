using Inlämning2.Core.Interfaces;
using Inlämning2.Data.Interfaces;
using Inlämning2.Domain.DTOs;
using Inlämning2.Domain.Entities;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Inlämning2.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IUserRepository _userRepo;

        public OrderService(IOrderRepository repo, IProductRepository productRepo, ICategoryRepository categoryRepo, IUserRepository userRepo)
        {
            _repo = repo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _userRepo = userRepo;
        }

        public async Task Create(List<OrderProductDTO> orderDTO, int userID)
        {
            var orderProducts = orderDTO.Select(dto => new OrderProduct
            {
                ProductID = dto.ProductID,
                Quantity = dto.Quantity

            }).ToList();

            var totalPrice = 0;

            foreach (var o in orderProducts)
            {
                var products = await _productRepo.GetProduct(o.ProductID);
                if(products == null)
                {
                    throw new Exception("Not a valid productID");
                }
                totalPrice += products.Price * o.Quantity;
            }

            var order = new Order
            {
                OrderProducts = orderProducts,
                UserID = userID,
                User = await _userRepo.GetById(userID),
                TotalPrice = totalPrice
            };

            await _repo.Create(order);
        }

        public async Task<List<OrderDTO>> GetAllDtoOrders(int? userID)
        {
            //List<Order> orders = await _repo.GetAllOrders();
            List<Order> orders = await _repo.GetAllOrdersForUser(userID);
            List<OrderDTO> orderDTOs = await MapperDTO(orders);
            if(orders.Count() == 0)
            {
                throw new Exception("You have no placed orders.");
            }
            return orderDTOs;
        }

        public async Task<List<OrderDTO>> MapperDTO(List<Order> orders)
        {
            var orderDTOs = orders.Select(o => new OrderDTO
            {
                
                OrderID = o.OrderID,
                UserID = o.UserID,
                Products = o.OrderProducts.Select(p => new ProductDTO
                {
                    Title = p.Product.Title,
                    Price = p.Product.Price,
                    Category = p.Product.Category.Title

                }).ToList(),
                TotalPrice = o.TotalPrice 
            }).ToList();

            return await Task.FromResult(orderDTOs);
        }
    }
}
