using BlockchainApp.Services.MiningServ;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiningController : ControllerBase
    {
        private readonly IMiningService _miningService;

        public MiningController(IMiningService miningService)
        {
            _miningService = miningService;
        }

        [HttpPost("mine")]
        public async Task<IActionResult> Mine()
        {
            var block = await _miningService.MineBlockAsync();
            if (block == null)
            {
                return BadRequest("No transactions to mine.");
            }
            return Ok(block);
        }
    }
}
