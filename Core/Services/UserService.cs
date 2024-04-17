using Inlämning2.Core.Interfaces;
using Inlämning2.Data.Interfaces;
using Inlämning2.Domain.DTOs;
using Inlämning2.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inlämning2.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        //Saknar IF-logik annars klar async-metod.
        public async Task Create(User user)
        {
            var userExist = await _repo.GetByUsername(user.Username);
            if (userExist == null)
            {
                await _repo.Create(user);
            }
            else
            {
                throw new Exception("Username already exists.");
            }
        }

        public async Task<User> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<string> LogIn(UserDTO userDTO)
        {
            var user = await _repo.GetByUsername(userDTO.Username);
            if(user != null && user.Password == userDTO.Password)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));
                
                //claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#123456789101112"));
                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5171/",
                    audience: "http://localhost:5171/",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signInCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return tokenString;
                
            }
            else
            {
                throw new Exception("Fel inloggningsuppgifter.");
            }
        }

    }
}
