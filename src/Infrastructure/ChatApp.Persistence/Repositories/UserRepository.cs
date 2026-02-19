namespace ChatApp.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context)
    : GenericRepository<AppUser>(context), IUserRepository
{
}