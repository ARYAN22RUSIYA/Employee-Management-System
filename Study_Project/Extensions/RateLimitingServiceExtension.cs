using AspNetCoreRateLimit;

namespace Study_Project.Extensions
{
    public static class RateLimitingServiceExtension
    {
        public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache(); 

            int limit = configuration.GetValue<int>("RateLimiting:Limit");
            string period = configuration.GetValue<string>("RateLimiting:Period");

            if (limit <= 0 || string.IsNullOrEmpty(period))
            {
                throw new Exception("Rate limiting settings are missing or incorrect in appsettings.json");
            }

            services.Configure<IpRateLimitOptions>(options =>
            {
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = "*", 
                        Limit = limit,  
                        Period = period 
                    }
                };
            });

            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            return services;
        }
    }
}
