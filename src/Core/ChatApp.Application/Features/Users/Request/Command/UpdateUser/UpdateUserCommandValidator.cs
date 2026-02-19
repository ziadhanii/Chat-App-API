namespace ChatApp.Application.Features.Users.Handler.Command.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommandRequest>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters")
            .When(x => !string.IsNullOrEmpty(x.UserName));

        RuleFor(x => x.Bio)
            .MaximumLength(500).WithMessage("Bio must not exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Bio));

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City must not exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.City));

        RuleFor(x => x.Country)
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.Country));

        RuleFor(x => x.Interests)
            .MaximumLength(500).WithMessage("Interests must not exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Interests));
    }
}
