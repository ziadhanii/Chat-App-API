namespace ChatApp.Application.Features.Users.Handler.Command.AddUser;

public record AddUserCommandRequest(
    string Email,
    string UserName,
    string Password,
    IFormFile Image,
    string? Bio,
    string? City,
    string? Country,
    string? Interests
) : IRequest<Result<UserResponse>>;
