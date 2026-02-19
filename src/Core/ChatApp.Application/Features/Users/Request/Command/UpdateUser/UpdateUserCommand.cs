namespace ChatApp.Application.Features.Users.Handler.Command.UpdateUser;

public record UpdateUserCommand(
    int UserId,
    UpdateUserCommandRequest Request
) : IRequest<Result<UserResponse>>;
