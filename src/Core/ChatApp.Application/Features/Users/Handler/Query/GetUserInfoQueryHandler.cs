using ChatApp.Application.Features.Users.Request.Query;

namespace ChatApp.Application.Features.Users.Handler.Query;

public class GetUserInfoQueryHandler(IUserRepository repo, IMapper mapper)
    : IRequestHandler<GetUserInfoQuery, Result<UserResponse>>
{
    public async Task<Result<UserResponse>> Handle(
        GetUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var user = await repo.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.NotFound);

        var response = mapper.Map<UserResponse>(user);

        return Result.Success(response);
    }
}