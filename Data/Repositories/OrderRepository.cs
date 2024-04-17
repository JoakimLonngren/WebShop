using Inlämning2.Data.Context;
using Inlämning2.Data.Interfaces;
using Inlämning2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inlämning2.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebShopContext _context;

        public OrderRepository(WebShopContext context)
        {
            _context = context;
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(o => o.OrderProducts)
                                        .ThenInclude(op => op.Product)
                                        .ThenInclude(p => p.Category)
                                        .Include(o => o.User).ToListAsync();                
        }

        public async Task<List<Order>> GetAllOrdersForUser(int? userID)
        {
            return await _context.Orders.Include(o => o.OrderProducts)
                                        .ThenInclude(op => op.Product)
                                        .ThenInclude(p => p.Category)
                                        .Include(o => o.User)
                                        .Where(o => o.UserID == userID).ToListAsync();
        }
    }
}
