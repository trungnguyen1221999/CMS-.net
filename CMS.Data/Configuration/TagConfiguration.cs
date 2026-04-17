using CMS.Core.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Configurations
{
    /// <summary>
    /// Database configuration for the Tag entity using Fluent API.
    /// </summary>
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            // 1. Table mapping
            builder.ToTable("Tags");

            // 2. Primary Key configuration
            builder.HasKey(x => x.Id);

            // 3. Name configuration: Max length 100 and NOT NULL
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            // 4. Indexing: Creating an index on Name to speed up tag-based searches
            builder.HasIndex(x => x.Name);

            builder.HasMany(x => x.PostTags)
                   .WithOne(pt => pt.Tag)
                   .HasForeignKey(pt => pt.TagId);
        }
    }
}