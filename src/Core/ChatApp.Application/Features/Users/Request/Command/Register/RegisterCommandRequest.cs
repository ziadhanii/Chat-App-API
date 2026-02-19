namespace ChatApp.Application.Features.Users.Handler.Command.Register;

public record RegisterCommandRequest(
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword,
    string? FirstName,
    string? LastName,
    string? Bio,
    string? City,
    string? Country,
    string? Interests
) : IRequest<Result<AuthResponse>>;
