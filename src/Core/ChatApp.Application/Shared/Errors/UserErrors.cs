namespace ChatApp.Application.Shared.Errors;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "User not found",
        StatusCodes.Status404NotFound);

    public static readonly Error AlreadyExists = new(
        "User.AlreadyExists",
        "User with this email already exists",
        StatusCodes.Status409Conflict);

    public static readonly Error UsernameAlreadyTaken = new(
        "User.UsernameAlreadyTaken",
        "Username is already taken",
        StatusCodes.Status409Conflict);

    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Invalid email or password",
        StatusCodes.Status401Unauthorized);

    public static readonly Error Blocked = new(
        "User.Blocked",
        "User is blocked",
        StatusCodes.Status403Forbidden);
}