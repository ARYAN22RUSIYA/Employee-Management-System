using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Study_Project.Extensions
{
    public static class LoggingExtension
    {
        public static void AddSerilogLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration) 
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
