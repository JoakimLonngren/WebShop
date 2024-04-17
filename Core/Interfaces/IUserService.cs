using Inlämning2.Domain.DTOs;
using Inlämning2.Domain.Entities;

namespace Inlämning2.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetById(int id);

        Task Create(User user);
        Task<string> LogIn(UserDTO userDTO);
    }
}
