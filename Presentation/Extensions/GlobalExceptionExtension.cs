using Microsoft.AspNetCore.Builder;

namespace Study_Project.Extensions
{
    public static class GlobalExceptionExtension
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Middleware.GlobalExceptionMiddleware>();
        }
    }
}
