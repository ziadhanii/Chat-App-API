namespace ChatApp.Application.Features.Users.Handler.Command.Register;

public class RegisterCommandHandler(
    UserManager<AppUser> userManager,
    IJwtProvider jwtProvider)
    : IRequestHandler<RegisterCommandRequest, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(
        RegisterCommandRequest request,
        CancellationToken cancellationToken)
    {
        var existingUserByEmail = await userManager.FindByEmailAsync(request.Email);

        if (existingUserByEmail is not null)
            return Result.Failure<AuthResponse>(UserErrors.AlreadyExists);

        var existingUserByName = await userManager.FindByNameAsync(request.UserName);

        if (existingUserByName is not null)
            return Result.Failure<AuthResponse>(UserErrors.UsernameAlreadyTaken);

        var user = new AppUser
        {
            Email = request.Email,
            UserName = request.UserName,
            LastActive = DateTime.UtcNow,
            PresenceStatus = PresenceStatus.Online,
            Bio = request.Bio,
            City = request.City,
            Country = request.Country,
            Interests = request.Interests
        };

        var identityResult = await userManager.CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
        {
            var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            return Result.Failure<AuthResponse>(
                new Error("User.CreationFailed", errors, 400));
        }

        // Assign default User role
        await userManager.AddToRoleAsync(user, "User");

        var roles = await userManager.GetRolesAsync(user);
        var permissions = GetPermissions(roles);

        var (token, expiresIn) = jwtProvider.GenerateToken(user, roles, permissions);

        var response = new AuthResponse(
            user.Id,
            user.UserName ?? string.Empty,
            user.Email ?? string.Empty,
            token,
            expiresIn,
            user.ImageUrl,
            user.PresenceStatus.ToString()
        );

        return Result.Success(response);
    }

    private IEnumerable<string> GetPermissions(IEnumerable<string> roles)
    {
        var permissionsDictionary = new Dictionary<string, IEnumerable<string>>
        {
            ["Admin"] = ["create", "read", "update", "delete"],
            ["User"] = ["read", "update"],
            ["Guest"] = ["read"]
        };

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            if (permissionsDictionary.TryGetValue(role, out var rolePermissions))
            {
                permissions.AddRange(rolePermissions);
            }
        }

        return permissions.Distinct();
    }
}
