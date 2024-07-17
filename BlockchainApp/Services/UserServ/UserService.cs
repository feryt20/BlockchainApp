using BlockchainApp.Db;
using BlockchainApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlockchainApp.Services.UserServ
{
    public class UserService : IUserService
    {
        private readonly BlockchainContext _context;

        public UserService(BlockchainContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(string password)
        {
            var user = new User
            {
                Address = GenerateAddress(),
                PasswordHash = HashPassword(password),
                Balance = 1000
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByAddressAsync(string address)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Address == address);
        }

        private string GenerateAddress()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
