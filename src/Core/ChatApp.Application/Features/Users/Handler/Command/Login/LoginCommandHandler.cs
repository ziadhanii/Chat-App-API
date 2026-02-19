namespace ChatApp.Application.Features.Users.Handler.Command.Login;

public class LoginCommandHandler(
    UserManager<AppUser> userManager,
    IJwtProvider jwtProvider)
    : IRequestHandler<LoginCommandRequest, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(
        LoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.NotFound);

        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
            return Result.Failure<AuthResponse>(
                new Error("Auth.InvalidPassword", "Invalid password", StatusCodes.Status401Unauthorized));

        var roles = await userManager.GetRolesAsync(user);
        var permissions = GetPermissions(roles);

        var (token, expiresIn) = jwtProvider.GenerateToken(user, roles, permissions);

        user.LastActive = DateTime.UtcNow;
        user.PresenceStatus = PresenceStatus.Online;

        await userManager.UpdateAsync(user);

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
