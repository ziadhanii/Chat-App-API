namespace ChatApp.Application.Features.Users.Request.Query;

public sealed record GetUserInfoQuery(int UserId)
    : IRequest<Result<UserResponse>>;