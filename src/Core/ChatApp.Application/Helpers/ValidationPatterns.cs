namespace ChatApp.Application.Helpers;

public static class ValidationPatterns
{
    public const string StrongPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
}