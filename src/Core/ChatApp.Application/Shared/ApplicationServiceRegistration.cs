namespace ChatApp.Application.Shared;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                typeof(ApplicationServiceRegistration).Assembly);
        });

        // Register Mapster with isolated config instance

        var mappingConfig = new TypeAdapterConfig();

        mappingConfig.Scan(
            typeof(ApplicationServiceRegistration).Assembly);

        services.AddSingleton(mappingConfig);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}