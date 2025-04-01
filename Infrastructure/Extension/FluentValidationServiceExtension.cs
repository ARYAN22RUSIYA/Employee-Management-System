using Infrastructure.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class FluentValidationServiceExtension
    {
        public static IServiceCollection AddFluentValidationPipeline(this IServiceCollection services)
        {
            // Register FluentValidation pipeline behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

            return services;
        }
    }
}
