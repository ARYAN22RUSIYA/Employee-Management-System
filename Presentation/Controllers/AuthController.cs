using Application.Auth.Commands.AssignRole;
using Application.Auth.Queries.GetRoles;
using Application.Features.Auth.Commands.AddRole;
using Application.Features.Auth.Commands.LoginUser;
using Application.Features.Auth.Commands.RegisterUser;
using Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Study_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var result = await _mediator.Send(new RegisterUserCommand(model));
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var result = await _mediator.Send(new LoginUserCommand(model));
            return Ok(result);
        }

        [HttpPost("add-role")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            var result = await _mediator.Send(new AddRoleCommand(role));
            return Ok(result);
        }

        [HttpPost("assign-role")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            var result = await _mediator.Send(new AssignRoleCommand(model));
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetRolesQuery());
            return Ok(result);
        }

        /*
        [HttpGet("debug-claims")]
        public IActionResult DebugClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }
        */
    }
}
