namespace ChatApp.Domain.Entities;

public class BlockedUser : BaseEntity<int>
{
    public int BlockerId { get; set; }

    public AppUser Blocker { get; set; } = null!;

    public int BlockedId { get; set; }

    public AppUser Blocked { get; set; } = null!;
}