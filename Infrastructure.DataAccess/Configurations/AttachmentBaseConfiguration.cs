using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class AttachmentBaseConfiguration : IEntityTypeConfiguration<AttachmentBase>
{
    public void Configure(EntityTypeBuilder<AttachmentBase> builder)
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
    }
}