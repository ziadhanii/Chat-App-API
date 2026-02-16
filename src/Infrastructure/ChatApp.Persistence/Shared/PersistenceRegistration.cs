using Microsoft.Extensions.Configuration;

namespace ChatApp.Persistence.Shared;

public static class PersistenceRegistration
{
    public static void ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<AppUser, IdentityRole<int>>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}