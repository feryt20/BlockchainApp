namespace BlockchainApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string PasswordHash { get; set; }
        public string PrivateKey { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
