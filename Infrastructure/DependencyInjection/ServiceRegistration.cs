using Microsoft.Extensions.DependencyInjection;
using Core.Interface;
using Infrastructure.Persistence;
using Infrastructure.Helpers;
namespace Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        /// <summary>
        /// Registers infrastructure services, such as repositories. Email and background job services are registered in Program.cs.
        /// </summary>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register EmployeeRepository for dependency injection
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            // Email and background job services are registered in Program.cs
            return services;
        }
    }
} 