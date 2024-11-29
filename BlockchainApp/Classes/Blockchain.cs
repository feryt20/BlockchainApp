//using BlockchainApp.Classes;
//using BlockchainApp.Models;
//using System.Security.Cryptography;
//using System.Text;

//namespace BlockchainApp.Classes
//{
//    public class Blockchain
//    {
//        public IList<Block> Chain { get; set; }
//        public IList<Transaction> PendingTransactions { get; set; }
//        // Add other properties and methods as before
//        private ProofOfStake _proofOfStake;
//        public Blockchain()
//        {
//            Chain = new List<Block>();
//            PendingTransactions = new List<Transaction>();
//            _proofOfStake = new ProofOfStake();
//            CreateBlock(1, "0");  // Create the genesis block
//        }

//        public Block CreateBlock(int proof, string previousHash)
//        {
//            var block = new Block
//            {
//                Id = Chain.Count + 1,
//                Timestamp = DateTime.Now,
//                Transactions = new List<Transaction>(PendingTransactions),
//                Proof = proof,
//                PreviousHash = previousHash
//            };

//            PendingTransactions.Clear();
//            Chain.Add(block);
//            return block;
//        }

//        public Block GetLastBlock()
//        {
//            return Chain.Last();
//        }

//        public int AddTransaction(string sender, string recipient, decimal amount)
//        {
//            PendingTransactions.Add(new Transaction
//            {
//                FromAddress = sender,
//                ToAddress = recipient,
//                Amount = amount
//            });

//            return GetLastBlock().Id + 1;
//        }
//        public bool AddTransaction(string sender, string recipient, decimal amount, string signature, string publicKey)
//        {
//            var transaction = new Transaction
//            {
//                FromAddress = sender,
//                ToAddress = recipient,
//                Amount = amount,
//                Signature = signature
//            };

//            var data = $"{sender}{recipient}{amount}";
//            var digitalSignature = new DigitalSignature();
//            if (digitalSignature.VerifyData(data, publicKey, signature))
//            {
//                PendingTransactions.Add(transaction);
//                return true;
//            }

//            return false;
//        }

//        public int ProofOfWork(int? lastProof)
//        {
//            int proof = 0;
//            while (!IsValidProof(lastProof, proof))
//            {
//                proof++;
//            }

//            return proof;
//        }

//        public bool IsValidProof(int? lastProof, int? proof)
//        {
//            var guess = $"{lastProof}{proof}";
//            using (var sha256 = SHA256.Create())
//            {
//                var bytes = Encoding.UTF8.GetBytes(guess);
//                var hash = sha256.ComputeHash(bytes);
//                var hashString = Convert.ToBase64String(hash);
//                return hashString.StartsWith("0000");
//            }
//        }

//        public bool IsChainValid()
//        {
//            for (int i = 1; i < Chain.Count; i++)
//            {
//                var currentBlock = Chain[i];
//                var previousBlock = Chain[i - 1];

//                if (currentBlock.PreviousHash != previousBlock.ComputeHash())
//                {
//                    return false;
//                }

//                if (!IsValidProof(previousBlock.Proof, currentBlock.Proof))
//                {
//                    return false;
//                }
//            }

//            return true;
//        }



//        // Add other properties and methods as before
//        public decimal GetBalance(string address)
//        {
//            decimal balance = 0;

//            foreach (var block in Chain)
//            {
//                foreach (var transaction in block.Transactions)
//                {
//                    if (transaction.FromAddress == address)
//                    {
//                        balance -= transaction.Amount;
//                    }

//                    if (transaction.ToAddress == address)
//                    {
//                        balance += transaction.Amount;
//                    }
//                }
//            }

//            return balance;
//        }

//        public Block CreateBlock(int proof, string previousHash, string validator)
//        {
//            var block = new Block
//            {
//                Id = Chain.Count + 1,
//                Timestamp = DateTime.Now,
//                Transactions = new List<Transaction>(PendingTransactions),
//                Proof = proof,
//                PreviousHash = previousHash
//            };

//            PendingTransactions.Clear();
//            Chain.Add(block);

//            // Reward the validator
//            AddTransaction("0", validator, 1);

//            return block;
//        }
//    }
//}
