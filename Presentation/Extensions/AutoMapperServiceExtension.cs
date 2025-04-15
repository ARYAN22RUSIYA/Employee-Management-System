using Application; // <-- Important: make sure this using is there

public static class AutoMapperServiceExtension
{
    public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AssemblyReference).Assembly);
        return services;
    }
}
