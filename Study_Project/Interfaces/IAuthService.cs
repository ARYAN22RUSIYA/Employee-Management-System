using Study_Project.Models;

public interface IAuthService
{
    Task<object> RegisterAsync(Register model);
    Task<object> LoginAsync(Login model);
    Task<object> AddRoleAsync(string role);
    Task<object> AssignRoleAsync(UserRole model);
    Task<object> GetAllRolesAsync(); 
}
