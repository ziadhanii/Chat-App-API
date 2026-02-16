namespace ChatApp.Persistence.DatabaseContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<
        AppUser,
        IdentityRole<int>,
        int,
        IdentityUserClaim<int>,
        IdentityUserRole<int>,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>>(options)
{
    public virtual DbSet<Message> Messages { get; set; } = null!;
    public virtual DbSet<BlockedUser> BlockedUsers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<BlockedUser>()
            .HasOne(b => b.Blocker)
            .WithMany(u => u.BlockedUsers)
            .HasForeignKey(b => b.BlockerId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Entity<BlockedUser>()
            .HasOne(b => b.Blocked)
            .WithMany()
            .HasForeignKey(b => b.BlockedId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}