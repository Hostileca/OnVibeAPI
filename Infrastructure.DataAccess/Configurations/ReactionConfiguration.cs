using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

internal class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
{
    public void Configure(EntityTypeBuilder<Reaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Emoji)
            .IsRequired();
        
        builder
            .HasOne(x => x.Sender)
            .WithMany(x => x.Reactions)
            .HasForeignKey(x => x.SenderId)
            .IsRequired();
        
        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.Reactions)
            .HasForeignKey(x => x.MessageId)
            .IsRequired();
    }
}