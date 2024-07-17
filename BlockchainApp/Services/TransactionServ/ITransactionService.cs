using BlockchainApp.Models;

namespace BlockchainApp.Services.TransactionServ
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransactionAsync(string fromAddress, string toAddress, decimal amount, decimal fee, string? signature);
        Task<Transaction> CreateTransactionPublicKeyAsync(string fromAddress, string toAddress, decimal amount, decimal fee, string? signature, string? publicKey);
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<List<Transaction>> GetTransactionsByAddressAsync(string address);
    }
}
