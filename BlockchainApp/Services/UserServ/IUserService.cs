using BlockchainApp.Models;

namespace BlockchainApp.Services.UserServ
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string password);
        Task<User> GetUserByAddressAsync(string address);
    }
}
