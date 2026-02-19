namespace ChatApp.Application.Features.Users.Handler.Command.UpdateUser;

public record UpdateUserCommandRequest(
    string? UserName,
    string? Bio,
    string? City,
    string? Country,
    string? Interests,
    IFormFile? Image
);
