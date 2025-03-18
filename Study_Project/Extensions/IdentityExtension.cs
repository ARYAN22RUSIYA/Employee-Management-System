using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Study_Project.Context;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Study_Project.Extensions
{
    public static class IdentityExtension
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

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                };
            });

            return services;
        }
    }
}
