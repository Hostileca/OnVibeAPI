using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.HasKey(x => new { x.ChatId, x.UserId });
        
        builder
            .HasOne(x => x.Chat)
            .WithMany(x => x.ChatsMembers)
            .HasForeignKey(x => x.ChatId)
            .IsRequired();
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.ChatsMembers)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
    }
}