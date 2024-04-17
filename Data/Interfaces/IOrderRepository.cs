using Inlämning2.Data.Context;
using Inlämning2.Domain.Entities;

namespace Inlämning2.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task Create (Order order);
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetAllOrdersForUser(int? userID);

    }
}
