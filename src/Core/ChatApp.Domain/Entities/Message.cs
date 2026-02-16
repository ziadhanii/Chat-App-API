namespace ChatApp.Domain.Entities;

public class Message : BaseEntity<int>
{
    public int SenderId { get; set; }

    public AppUser Sender { get; set; } = null!;

    public int ReceiverId { get; set; }

    public AppUser Receiver { get; set; } = null!;

    public string Content { get; set; } = string.Empty;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public bool IsRead { get; set; }

    public DateTime? ReadAt { get; set; }

    public bool IsBlock { get; set; }
}