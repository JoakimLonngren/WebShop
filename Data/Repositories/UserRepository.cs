using Inlämning2.Data.Context;
using Inlämning2.Data.Interfaces;
using Inlämning2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inlämning2.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly WebShopContext _context;

        public UserRepository(WebShopContext context)
        {
            _context = context;
        }

        //Klar async-metod för att lägga till en användare
        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> LogIn(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            
        }
    }
}
