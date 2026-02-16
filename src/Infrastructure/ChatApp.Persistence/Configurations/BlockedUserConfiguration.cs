namespace ChatApp.Persistence.Configurations;

public class BlockedUserConfiguration : IEntityTypeConfiguration<BlockedUser>
{
    public void Configure(EntityTypeBuilder<BlockedUser> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BlockerId)
            .IsRequired();

        builder.Property(x => x.BlockedId)
            .IsRequired();

        builder.HasOne(b => b.Blocker)
            .WithMany(u => u.BlockedUsers)
            .HasForeignKey(b => b.BlockerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Blocked)
            .WithMany()
            .HasForeignKey(b => b.BlockedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => new { b.BlockerId, b.BlockedId })
            .IsUnique();

        builder.HasIndex(b => b.BlockerId);
        builder.HasIndex(b => b.BlockedId);
    }
}