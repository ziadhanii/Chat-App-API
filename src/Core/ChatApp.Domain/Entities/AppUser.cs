namespace ChatApp.Domain.Entities;

public abstract class AppUser : IdentityUser<int>
{
    public string? ImageUrl { get; set; }

    public string? Bio { get; set; }

    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    public PresenceStatus PresenceStatus { get; set; } = PresenceStatus.Offline;

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Interests { get; set; }
}