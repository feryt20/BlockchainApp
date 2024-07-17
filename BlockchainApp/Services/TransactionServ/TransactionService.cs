using BlockchainApp.Classes;
using BlockchainApp.Db;
using BlockchainApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlockchainApp.Services.TransactionServ
{
    public class TransactionService : ITransactionService
    {
        private readonly BlockchainContext _context;
        //public IList<Transaction> PendingTransactions { get; set; }
        public TransactionService(BlockchainContext context)
        {
            //PendingTransactions = new List<Transaction>();
            _context = context;
        }

        public async Task<Transaction> CreateTransactionAsync(string fromAddress, string toAddress, decimal amount, decimal fee, string? signature)
        {
            var fromUser = await _context.Users.SingleOrDefaultAsync(u => u.Address == fromAddress);
            var toUser = await _context.Users.SingleOrDefaultAsync(u => u.Address == toAddress);

            if (fromUser == null || toUser == null || fromUser.Balance < amount + fee)
            {
                return null;
            }

            fromUser.Balance -= (amount + fee);
            toUser.Balance += amount;

            var transaction = new Transaction
            {
                FromAddress = fromAddress,
                ToAddress = toAddress,
                Amount = amount,
                Fee = fee,
                Date = DateTime.UtcNow,
                Confirmed = false,
                BlockId = null, // Initially, no block is associated
                Signature = signature
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<List<Transaction>> GetTransactionsByAddressAsync(string address)
        {
            return await _context.Transactions
                .Where(t => t.FromAddress == address || t.ToAddress == address)
                .ToListAsync();
        }


        public async Task<Transaction> CreateTransactionPublicKeyAsync(string fromAddress, string toAddress, decimal amount, decimal fee, string? signature, string? publicKey)
        {
            var fromUser = await _context.Users.SingleOrDefaultAsync(u => u.Address == fromAddress);
            var toUser = await _context.Users.SingleOrDefaultAsync(u => u.Address == toAddress);

            if (fromUser == null || toUser == null || fromUser.Balance < amount + fee)
            {
                return null;
            }

            fromUser.Balance -= (amount + fee);
            toUser.Balance += amount;

            var transaction = new Transaction
            {
                FromAddress = fromAddress,
                ToAddress = toAddress,
                Amount = amount,
                Fee = fee,
                Date = DateTime.UtcNow,
                Confirmed = false,
                BlockId = null, // Initially, no block is associated
                Signature = signature,
                PublicKey = publicKey
            };

            var data = $"{fromAddress}{toAddress}{amount}";
            var digitalSignature = new DigitalSignature();
            if (digitalSignature.VerifyData(data, publicKey, signature))
            {
                //PendingTransactions.Add(transaction);
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return transaction;
            }

            return null;
        }

    }
}
