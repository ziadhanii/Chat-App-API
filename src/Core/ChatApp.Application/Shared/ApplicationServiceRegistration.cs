namespace ChatApp.Application.Shared;

public static class ApplicationServiceRegistration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection ConfigureApplicationServices()
        {
            services.RegisterMediatR();
            services.RegisterFluentValidation();
            services.RegisterMapster();

            return services;
        }

        private IServiceCollection RegisterMediatR()
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(ApplicationServiceRegistration).Assembly);
            });

            return services;
        }

        private IServiceCollection RegisterFluentValidation()
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        private IServiceCollection RegisterMapster()
        {
            var mappingConfig = new TypeAdapterConfig();

            mappingConfig.Scan(
                typeof(ApplicationServiceRegistration).Assembly);

            services.AddSingleton(mappingConfig);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}