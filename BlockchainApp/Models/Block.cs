using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainApp.Models
{
    public class Block
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public int? Proof { get; set; }
        public List<Transaction> Transactions { get; set; }

        public string ComputeHash()
        {
            var blockData = JsonConvert.SerializeObject(this);
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(blockData);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
