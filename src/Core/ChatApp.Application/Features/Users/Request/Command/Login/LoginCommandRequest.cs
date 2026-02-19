namespace ChatApp.Application.Features.Users.Handler.Command.Login;

public record LoginCommandRequest(
    string Email,
    string Password
) : IRequest<Result<AuthResponse>>;
