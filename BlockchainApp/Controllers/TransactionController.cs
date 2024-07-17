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
            var transaction = await _transactionService.CreateTransactionAsync(dto.FromAddress, dto.ToAddress, dto.Amount, dto.Fee,dto.Signature);
            if (transaction == null)
            {
                return BadRequest("Transaction failed.");
            }
            return Ok(transaction);
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
