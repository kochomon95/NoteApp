using Microsoft.AspNetCore.Mvc;
using Note_App.IRepositories;
using Note_App.Models;
using Note_App.Repositories;

namespace Note_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("userregis")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid user data.");
            }

            var result = await _userRepository.RegisterUserAsync(user);

            if (result == null)
            {
                return StatusCode(500, "Error saving user to the database.");
            }

            return Ok(result);
        }
    }
}
