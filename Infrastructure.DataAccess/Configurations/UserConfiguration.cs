using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .HasIndex(x => x.Email)
            .IsUnique();
        
        builder
            .HasIndex(x => x.Username)
            .IsUnique();
        
        builder
            .Property(x => x.HashedPassword)
            .IsRequired();
        
        builder
            .Property(x => x.RefreshToken);
        
        builder
            .Property(x => x.BIO);
        
        builder
            .Property(x => x.DateOfBirth);
        
        builder
            .Property(x => x.City);
        
        builder
            .Property(x => x.Country);
        
        builder
            .Property(x => x.Avatar);
        
        builder
            .Property(x => x.CreatedAt).IsRequired();
        
        builder
            .Property(x => x.Role).IsRequired();
        
        builder
            .HasMany(x => x.Messages)
            .WithOne(x => x.Sender)
            .HasForeignKey(x => x.SenderId);
        
        builder
            .HasMany(x => x.Reactions)
            .WithOne(x => x.Sender)
            .HasForeignKey(x => x.SenderId);
        
        builder
            .HasMany(x => x.Posts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
        
        builder
            .HasMany(x => x.Likes)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
        
        builder
            .HasMany(u => u.Subscriptions)
            .WithOne(s => s.SubscribedBy)
            .HasForeignKey(s => s.SubscribedById)
            .OnDelete(DeleteBehavior.Cascade); 

        builder
            .HasMany(u => u.Subscribers)
            .WithOne(s => s.SubscribedTo)
            .HasForeignKey(s => s.SubscribedToId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(x => x.Comments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder
            .HasMany(x => x.ChatsMembers)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}