namespace ChatApp.Domain.Entities;

public class BlockedUser : BaseEntity<int>
{
    public int BlockerId { get; set; }

    [ForeignKey(nameof(BlockerId))] public AppUser Blocker { get; set; } = null!;

    public int BlockedId { get; set; }

    [ForeignKey(nameof(BlockedId))] public AppUser Blocked { get; set; } = null!;
}