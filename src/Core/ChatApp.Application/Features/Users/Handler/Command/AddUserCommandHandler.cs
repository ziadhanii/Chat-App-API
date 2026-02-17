namespace ChatApp.Application.Features.Users.Handler.Command;

public class AddUserCommandHandler(
    UserManager<AppUser> userManager,
    IFileService fileService,
    IMapper mapper)
    : IRequestHandler<AddUserCommandRequest, Result<UserResponse>>
{
    public async Task<Result<UserResponse>> Handle(
        AddUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var existingUserByEmail = await userManager.FindByEmailAsync(request.Email);

        if (existingUserByEmail is not null)
            return Result.Failure<UserResponse>(UserErrors.AlreadyExists);

        var existingUserByName = await userManager.FindByNameAsync(request.UserName);

        if (existingUserByName is not null)
            return Result.Failure<UserResponse>(UserErrors.UsernameAlreadyTaken);

        string? imageUrl = null;
        if (request.Image is not null)
        {
            imageUrl = await fileService.UploadImageAsync(request.Image, request.Email, cancellationToken);
        }

        var user = mapper.Map<AppUser>(request);

        user.ImageUrl = imageUrl;
        user.LastActive = DateTime.UtcNow;
        user.PresenceStatus = PresenceStatus.Online;

        var identityResult = await userManager.CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
        {
            if (imageUrl is not null)
            {
                await fileService.DeleteImageAsync(imageUrl, cancellationToken);
            }

            var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            return Result.Failure<UserResponse>(
                new Error("User.CreationFailed", errors, 400));
        }

        var response = mapper.Map<UserResponse>(user);

        return Result.Success(response);
    }
}