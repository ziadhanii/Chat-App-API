namespace ChatApp.Application.Features.Users.Handler.Command;

public record AuthResponse(
    int Id,
    string UserName,
    string Email,
    string Token,
    int ExpiresIn,
    string? ImageUrl,
    string PresenceStatus
);
