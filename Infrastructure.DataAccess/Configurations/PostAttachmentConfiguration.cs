using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class PostAttachmentConfiguration : IEntityTypeConfiguration<PostAttachment>
{
    public void Configure(EntityTypeBuilder<PostAttachment> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Data)
            .IsRequired();
        
        builder
            .Property(x => x.FileName)
            .IsRequired();
        
        builder
            .Property(x => x.ContentType)
            .IsRequired();
        
        builder.HasOne(x => x.Post)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.PostId);
    }
}