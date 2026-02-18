namespace ChatApp.Application.Features.Users.Request;

public record UserResponse(
    int Id,
    string UserName,
    string Email,
    string? ImageUrl,
    string? Bio,
    string? City,
    string? Country,
    string? Interests,
    DateTime LastActive,
    string PresenceStatus
);