using BlockchainApp.Classes;
using BlockchainApp.Db;
using BlockchainApp.Models;
using BlockchainApp.Services.MiningServ;
using BlockchainApp.Services.TransactionServ;
using BlockchainApp.Services.UserServ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlockchainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        private readonly IMiningService _miningService;
        private readonly BlockchainContext _context;

        public BlockchainController(IUserService userService, ITransactionService transactionService, IMiningService miningService, BlockchainContext context)
        {
            _userService = userService;
            _transactionService = transactionService;
            _miningService = miningService;
            _context = context;
        }

        [HttpPost("generatekey")]
        public ActionResult<object> GenerateKeys([FromBody] CreateTransactionDto transaction)
        {
            var (publicKey, privateKey) = RSAKeyGenerator.GenerateKeys();
            var digitalSignature = new DigitalSignature();
            var data = $"{transaction.FromAddress}{transaction.ToAddress}{transaction.Amount}";
            var signature = digitalSignature.SignData(data, privateKey);
            return Ok(new { publicKey, privateKey, signature });
        }

        [HttpGet("mine")]
        public async Task<ActionResult<Block>> Mine()
        {
            var block = await _miningService.MineBlockAsync();
            if (block == null)
            {
                return BadRequest("No transactions to mine.");
            }
            return Ok(block);
        }
        
        [HttpPost("transactions/new")]
        public async Task<ActionResult<object>> CreateTransaction([FromBody] CreateTransactionDto transaction)
        {
            var success = await _transactionService.CreateTransactionAsync(transaction.FromAddress, transaction.ToAddress, transaction.Amount, transaction.Fee, transaction.Signature);
            if (success != null)
            {
                return Ok(new { message = $"Transaction will be added to Block {success.BlockId + 1}" });
            }
            else
            {
                return BadRequest(new { message = "Invalid transaction" });
            }
        }

        [HttpGet("chain")]
        public async Task<ActionResult<object>> GetChain()
        {
            var chain = await _context.Blocks.Include(b => b.Transactions).ToListAsync();
            var response = new
            {
                chain,
                length = chain.Count
            };

            return Ok(response);
        }

        [HttpPost("transactions/new2")]
        public async Task<ActionResult<object>> CreateTransactionNew2([FromBody] CreateTransactionDto transaction)
        {
            var success = await _transactionService.CreateTransactionPublicKeyAsync(transaction.FromAddress, transaction.ToAddress, transaction.Amount, transaction.Fee, transaction.Signature, transaction.PublicKey);

            if (success != null)
            {
                //return Ok(new { message = $"Transaction will be added to Block {Blockchain.GetLastBlock().Id + 1}" });
                return Ok(new { message = $"Transaction will be added to Block {success.BlockId + 1}" });
            }
            else
            {
                return BadRequest(new { message = "Invalid transaction" });
            }
        }

    }

}
