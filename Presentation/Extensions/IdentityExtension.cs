using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Study_Project.Context;

public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<JwtContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Database")));

        services.Configure<IdentityOptions>(config.GetSection("IdentityOptions"));

        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<JwtContext>()
                .AddDefaultTokenProviders();

        return services;
    }
}
