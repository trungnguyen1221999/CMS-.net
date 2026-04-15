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

            // Link to Post: If a Post is deleted, remove its tag associations
            builder.HasOne(x => x.Post)
                .WithMany() // Or .WithMany(p => p.PostTags) if defined in Post
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Link to Tag: If a Tag is deleted, remove it from all associated posts
            builder.HasOne(x => x.Tag)
                .WithMany() // Or .WithMany(t => t.PostTags) if defined in Tag
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}