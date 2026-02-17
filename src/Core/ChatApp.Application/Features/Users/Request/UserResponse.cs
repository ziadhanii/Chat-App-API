namespace ChatApp.Application.Features.Users.Request;

public class UserResponse
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Bio { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Interests { get; set; }
    public DateTime LastActive { get; set; }
    public string PresenceStatus { get; set; } = string.Empty;
}
