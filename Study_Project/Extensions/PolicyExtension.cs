using Microsoft.Extensions.DependencyInjection;

namespace Study_Project.Extensions
{
    public static class PolicyExtension
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
            });

            return services;
        }
    }
}
