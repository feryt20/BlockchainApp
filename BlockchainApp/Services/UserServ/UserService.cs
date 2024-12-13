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
                PrivateKey = GenerateAddress(),
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

        public async Task<string?> GetUserPrivateKeyAsync(string address, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Address == address);
            if (user != null)
            {
                if (IsMatchPassword(password, user.PasswordHash))
                {
                    return user.PrivateKey;
                }
            }
            return null;
        }

        private string GenerateAddress()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool IsMatchPassword(string password, string hashKey)
        {
            bool isMatch = BCrypt.Net.BCrypt.Verify(password, hashKey);
            return isMatch;
        }
    }
}
