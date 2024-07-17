using System.ComponentModel.DataAnnotations.Schema;

namespace BlockchainApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public DateTime Date { get; set; }
        public bool Confirmed { get; set; }
        public int? BlockId { get; set; }
        public string? Signature { get; set; }
        public string? PublicKey { get; set; }

        [ForeignKey("BlockId")]
        public Block? Block { get; set; }
    }
}
