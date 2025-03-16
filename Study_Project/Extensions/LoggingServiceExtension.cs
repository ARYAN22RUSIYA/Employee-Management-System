using Serilog;

namespace Study_Project.Extensions
{
    public static class LoggingServiceExtension
    {
        public static void AddSerilogLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration) // ✅ Reads log settings from appsettings.json
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
