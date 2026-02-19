namespace ChatApp.Application.Features.Users.Handler.Command.UpdateUser;

public class UpdateUserCommandHandler(
    UserManager<AppUser> userManager,
    IFileService fileService,
    IMapper mapper)
    : IRequestHandler<UpdateUserCommand, Result<UserResponse>>
{
    public async Task<Result<UserResponse>> Handle(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = await userManager.FindByIdAsync(command.UserId.ToString());

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.NotFound);

        if (!string.IsNullOrEmpty(request.UserName) && request.UserName != user.UserName)
        {
            var existingUser = await userManager.FindByNameAsync(request.UserName);
            if (existingUser is not null)
                return Result.Failure<UserResponse>(UserErrors.UsernameAlreadyTaken);

            user.UserName = request.UserName;
        }

        user.Bio = request.Bio ?? user.Bio;
        user.City = request.City ?? user.City;
        user.Country = request.Country ?? user.Country;
        user.Interests = request.Interests ?? user.Interests;

        if (request.Image is not null)
        {
            if (!string.IsNullOrEmpty(user.ImageUrl))
            {
                await fileService.DeleteImageAsync(user.ImageUrl, cancellationToken);
            }

            var imageUrl = await fileService.UploadImageAsync(request.Image, user.Email!, cancellationToken);
            user.ImageUrl = imageUrl;
        }

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure<UserResponse>(
                new Error("User.UpdateFailed", errors, 400));
        }

        var response = mapper.Map<UserResponse>(user);

        return Result.Success(response);
    }
}
