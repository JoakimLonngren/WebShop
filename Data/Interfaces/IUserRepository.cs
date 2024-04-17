using Inlämning2.Domain.Entities;

namespace Inlämning2.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task Create(User user);
        Task<User> GetByUsername(string username);
        Task<User> LogIn(string username, string password);
    }
}
