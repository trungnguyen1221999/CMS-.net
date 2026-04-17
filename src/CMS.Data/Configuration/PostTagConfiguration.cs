using CMS.Core.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Configurations
{
    /// <summary>
    /// Database configuration for PostTag using Fluent API.
    /// Configures the link between posts and their respective tags.
    /// </summary>
    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            // 1. Map to table name
            builder.ToTable("PostTags");

            // 2. Define Composite Primary Key (PostId + TagId)
            // This prevents the same tag from being assigned to the same post twice
            builder.HasKey(x => new { x.PostId, x.TagId });

            // 3. Configure Relationships

            // Link to Post: Phải trỏ rõ vào p.PostTags
            builder.HasOne(x => x.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Link to Tag: Phải trỏ rõ vào t.PostTags
            builder.HasOne(x => x.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}