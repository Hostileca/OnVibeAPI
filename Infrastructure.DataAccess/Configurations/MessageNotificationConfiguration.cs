using Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class MessageNotificationConfiguration : IEntityTypeConfiguration<MessageNotification>
{
    public void Configure(EntityTypeBuilder<MessageNotification> builder)
    {
        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.MessageId)
            .IsRequired();
    }
}