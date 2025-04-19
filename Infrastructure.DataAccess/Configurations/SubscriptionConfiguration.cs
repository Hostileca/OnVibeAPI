using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

internal class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(x => new { x.SubscribedById, x.SubscribedToId });

        builder
            .HasOne(s => s.SubscribedBy)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(s => s.SubscribedById)
            .IsRequired();

        builder
            .HasOne(s => s.SubscribedTo)
            .WithMany(u => u.Subscribers)
            .HasForeignKey(s => s.SubscribedToId)
            .IsRequired();
    }
}