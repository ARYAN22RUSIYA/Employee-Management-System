using Microsoft.AspNetCore.Mvc;
using Study_Project.Models;

namespace Study_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var result = await _authService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var result = await _authService.LoginAsync(model);
            return Ok(result);
        }

        [HttpPost("add-role")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            var result = await _authService.AddRoleAsync(role);
            return Ok(result);
        }

        [HttpPost("assign-role")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            var result = await _authService.AssignRoleAsync(model);
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _authService.GetAllRolesAsync();
            return Ok(result);
        }
    }
}
