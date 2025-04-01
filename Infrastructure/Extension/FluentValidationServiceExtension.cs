using FluentValidation;
using Infrastructure.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application;

namespace Infrastructure.Extensions
{
    public static class FluentValidationServiceExtension
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            // Register all validators from Application Project
            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

            // Register FluentValidation Behavior Pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

            return services;
        }
    }
}
