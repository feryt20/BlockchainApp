using BlockchainApp.Services.UserServ;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var user = await _userService.RegisterUserAsync(dto.Password);
            if (user == null)
            {
                return BadRequest("Registration failed.");
            }
            return Ok(new { user.Address });
        }

        [HttpGet("{address}")]
        public async Task<IActionResult> GetUser(string address)
        {
            var user = await _userService.GetUserByAddressAsync(address);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }

    public class RegisterUserDto
    {
        public string Password { get; set; }
    }
}
