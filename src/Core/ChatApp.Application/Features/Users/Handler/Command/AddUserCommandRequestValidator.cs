namespace ChatApp.Application.Features.Users.Handler.Command;

public class AddUserCommandRequestValidator : AbstractValidator<AddUserCommandRequest>
{
    public AddUserCommandRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(ValidationPatterns.StrongPassword);

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);


        RuleFor(x => x.Image)
            .Must(FileValidationHelper.IsValidImage);


        RuleFor(x => x.Bio)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Bio));

        RuleFor(x => x.City)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.City));

        RuleFor(x => x.Country)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.Country));

        RuleFor(x => x.Interests)
            .MaximumLength(300)
            .When(x => !string.IsNullOrWhiteSpace(x.Interests));
    }
}