using Inlämning2.Domain.DTOs;
using Inlämning2.Domain.Entities;

namespace Inlämning2.Core.Interfaces
{
    public interface IOrderService
    {
        Task Create(List<OrderProductDTO> productDTO, int userID);
        Task<List<OrderDTO>> GetAllDtoOrders(int? userID);
        Task <List<OrderDTO>> MapperDTO(List<Order>order);

    }
}
