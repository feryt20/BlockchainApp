using BlockchainApp.Db;
using BlockchainApp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainApp.Services.MiningServ
{
    public class MiningService : IMiningService
    {
        private readonly BlockchainContext _context;

        public MiningService(BlockchainContext context)
        {
            _context = context;
        }

        public async Task<Block> MineBlockAsync()
        {
            var pendingTransactions = await _context.Transactions
                .Where(t => !t.Confirmed)
                .ToListAsync();

            if (!pendingTransactions.Any())
            {
                return null;
            }

            var previousBlock = await _context.Blocks
                .OrderByDescending(b => b.Id)
                .FirstOrDefaultAsync();

            var newBlock = new Block
            {
                Timestamp = DateTime.UtcNow,
                PreviousHash = previousBlock?.Hash ?? string.Empty,
                Transactions = pendingTransactions,
                Proof = ProofOfWork(previousBlock?.Proof)
            };

            newBlock.Hash = ComputeHash(newBlock);
            _context.Blocks.Add(newBlock);
            await _context.SaveChangesAsync();

            foreach (var transaction in pendingTransactions)
            {
                transaction.Confirmed = true;
                transaction.BlockId = newBlock.Id;
            }

            await _context.SaveChangesAsync();

            return newBlock;
        }

        private string ComputeHash(Block block)
        {
            using (var sha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes($"{block.Timestamp}-{block.PreviousHash}-{JsonConvert.SerializeObject(block.Transactions)}");
                var outputBytes = sha256.ComputeHash(inputBytes);
                return Convert.ToBase64String(outputBytes);
            }
        }

        private int ProofOfWork(int? lastProof)
        {
            int proof = 0;
            while (!IsValidProof(lastProof, proof))
            {
                proof++;
            }

            return proof;
        }

        private bool IsValidProof(int? lastProof, int proof)
        {
            var guess = $"{lastProof}{proof}";
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(guess);
                var hash = sha256.ComputeHash(bytes);
                var hashString = Convert.ToBase64String(hash);
                return hashString.StartsWith("0000");
            }
        }
    }
}
