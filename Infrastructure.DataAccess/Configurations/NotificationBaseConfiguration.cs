using Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class NotificationBaseConfiguration : IEntityTypeConfiguration<NotificationBase>
{
    public void Configure(EntityTypeBuilder<NotificationBase> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .HasIndex(x => x.Date)
            .IsDescending();

        builder
            .Property(x => x.IsRead);
        
        builder
            .Property(x => x.Type);
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.UserId);
    }
}