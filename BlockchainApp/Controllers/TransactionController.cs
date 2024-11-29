using BlockchainApp.Services.TransactionServ;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] CreateTransactionDto dto)
        {
            var success = await _transactionService.CreateTransactionPublicKeyAsync(dto.FromAddress, dto.ToAddress, dto.Amount, dto.Fee, dto.Signature, dto.PublicKey);

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpGet("byAddress/{address}")]
        public async Task<IActionResult> GetTransactionsByAddress(string address)
        {
            var transactions = await _transactionService.GetTransactionsByAddressAsync(address);
            return Ok(transactions);
        }
    }

    public class CreateTransactionDto
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string? Signature { get; set; }
        public string? PublicKey { get; set; }
    }
}
