using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Core.DTOs;

namespace Study_Project.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<object> RegisterAsync(Register model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            // Given User Role on registration
            await _userManager.AddToRoleAsync(user, "User");
            return new { message = "User registered successfully" };
        }

        public async Task<object> LoginAsync(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username)
                       ?? throw new Exception("Invalid username or password");

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                throw new Exception("Invalid username or password");

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new { token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        public async Task<object> AddRoleAsync(string role)
        {
            if (await _roleManager.RoleExistsAsync(role))
                throw new Exception("Role already exists");

            var result = await _roleManager.CreateAsync(new IdentityRole(role));
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            return new { message = "Role added successfully" };
        }

        public async Task<object> AssignRoleAsync(UserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.Username)
                       ?? throw new Exception("User not found");

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            return new { message = "Role assigned successfully" };
        }

        public async Task<object> GetAllRolesAsync()
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();

            if (roles == null || !roles.Any())
                throw new Exception("No roles found");

            return new { roles };
        }

       
    }
}
