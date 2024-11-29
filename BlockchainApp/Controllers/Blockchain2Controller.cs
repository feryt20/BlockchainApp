//using BlockchainApp.Classes;
//using BlockchainApp.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BlockchainApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Blockchain2Controller : ControllerBase
//    {
//        private static readonly Blockchain Blockchain = new Blockchain();

//        [HttpPost("generatekey")]
//        public ActionResult<object> GenerateKeys([FromBody] Transaction transaction)
//        {
//            var (publicKey, privateKey) = RSAKeyGenerator.GenerateKeys();
//            var digitalSignature = new DigitalSignature();
//            var data = $"{transaction.FromAddress}{transaction.ToAddress}{transaction.Amount}";
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
//        public ActionResult<object> CreateTransaction([FromBody] Transaction transaction)
//        {
//            var index = Blockchain.AddTransaction(transaction.FromAddress, transaction.ToAddress, transaction.Amount);
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
//        public ActionResult<object> CreateTransactionNew([FromBody] Transaction transactionModel)
//        {
//            var success = Blockchain.AddTransaction(transactionModel.FromAddress, transactionModel.ToAddress, transactionModel.Amount, transactionModel.Signature, transactionModel.PublicKey);
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
