//using BlockchainApp.Classes;
//using BlockchainApp.Models;
//using BlockchainApp.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace BlockchainApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BlockchainController : ControllerBase
//    {
//        private static readonly Blockchain Blockchain = new Blockchain();

//        [HttpPost("generatekey")]
//        public ActionResult<object> GenerateKeys([FromBody] TransactionModel transaction)
//        {
//            var (publicKey, privateKey) = RSAKeyGenerator.GenerateKeys();
//            var digitalSignature = new DigitalSignature();
//            var data = $"{transaction.Sender}{transaction.Recipient}{transaction.Amount}";
//            var signature = digitalSignature.SignData(data, privateKey);
//            return Ok(new { publicKey, privateKey, signature });
//        }

//        [HttpGet("mine")]
//        public ActionResult<Block> Mine()
//        {
//            var lastBlock = Blockchain.GetLastBlock();
//            var lastProof = lastBlock.Proof;
//            var proof = Blockchain.ProofOfWork(lastProof);

//            Blockchain.AddTransaction(sender: "0", recipient: "myaddress1", amount: 1);

//            var previousHash = lastBlock.ComputeHash();
//            var block = Blockchain.CreateBlock(proof, previousHash);

//            return Ok(block);
//        }

//        [HttpPost("transactions/new1")]
//        public ActionResult<object> CreateTransaction([FromBody] TransactionModel transaction)
//        {
//            var index = Blockchain.AddTransaction(transaction.Sender, transaction.Recipient, transaction.Amount);
//            return Ok(new { message = $"Transaction will be added to Block {index}" });
//        }

//        [HttpGet("chain")]
//        public ActionResult<object> GetChain()
//        {
//            var response = new
//            {
//                chain = Blockchain.Chain,
//                length = Blockchain.Chain.Count
//            };

//            return Ok(response);
//        }

//        //*** contracts

//        [HttpPost("transactions/new")]
//        public ActionResult<object> CreateTransactionNew([FromBody] TransactionModel transactionModel)
//        {
//            var success = Blockchain.AddTransaction(transactionModel.Sender, transactionModel.Recipient, transactionModel.Amount, transactionModel.Signature, transactionModel.PublicKey);
//            if (success)
//            {
//                return Ok(new { message = $"Transaction will be added to Block {Blockchain.GetLastBlock().Id + 1}" });
//            }
//            else
//            {
//                return BadRequest(new { message = "Invalid transaction" });
//            }
//        }
//    }
//}
