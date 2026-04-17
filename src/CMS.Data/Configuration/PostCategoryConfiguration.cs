using CMS.Core.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Configurations
{
    /// <summary>
    /// Database configuration for PostCategory using Fluent API.
    /// </summary>
    public class PostCategoryConfiguration : IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            // 1. Map to table name
            builder.ToTable("PostCategories");

            // 2. Define Primary Key
            builder.HasKey(x => x.Id);

            // 3. Configure Name property
            builder.Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            // 4. Configure Slug property: Use varchar and set as Unique
            builder.Property(x => x.Slug)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.HasIndex(x => x.Slug)
                .IsUnique();

            // 5. Configure SEO and Audit fields
            builder.Property(x => x.SeoDescription)
                .HasMaxLength(160);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.SortOrder)
                .HasDefaultValue(0);

            // 6. Configure Self-Referencing Relationship (Hierarchy)
            // A category has one parent, and a parent can have many children
            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting parents that have sub-categories
        }
    }
}