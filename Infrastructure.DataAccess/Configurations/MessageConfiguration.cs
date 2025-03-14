using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Text)
            .HasMaxLength(1024);
        
        builder
            .HasIndex(x => x.Date)
            .IsDescending();
        
        builder
            .HasOne(x => x.Sender)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.SenderId)
            .IsRequired();
        
        builder
            .HasOne(x => x.Chat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ChatId)
            .IsRequired();

        builder
            .HasMany(x => x.Reactions)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId);

        builder
            .HasMany(x => x.Attachments)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId);
    }
}