namespace ChatApp.Persistence.Shared;

public static class PersistenceRegistration
{
    public static void ConfigurePersistenceServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole<int>>(op =>
            {
                op.Password.RequireNonAlphanumeric = true;
                op.Password.RequireLowercase = true;
                op.Password.RequireUppercase = true;
                op.Password.RequireDigit = true;
                op.SignIn.RequireConfirmedEmail = false;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}