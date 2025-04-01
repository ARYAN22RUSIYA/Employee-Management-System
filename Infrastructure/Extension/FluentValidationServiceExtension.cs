using FluentValidation;
using Infrastructure.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class FluentValidationServiceExtension
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(FluentValidationServiceExtension).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

            return services;
        }
    }
}
