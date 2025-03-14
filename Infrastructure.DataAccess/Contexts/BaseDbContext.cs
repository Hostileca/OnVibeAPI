using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public class BaseDbContext(DbContextOptions<BaseDbContext> options) : DbContext(options)
{
    public DbSet<Chat> Chats { get; init; }
    public DbSet<Comment> Comments { get; init; }
    public DbSet<Like> Likes { get; init; }
    public DbSet<Message> Messages { get; init; }
    public DbSet<MessageAttachment> MessageAttachments { get; init; }
    public DbSet<Post> Posts { get; init; }
    public DbSet<PostAttachment> PostAttachments { get; init; }
    public DbSet<Reaction> Reactions { get; init; }
    public DbSet<Subscription?> Subscriptions { get; init; }
    public DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext).Assembly);
    }
}