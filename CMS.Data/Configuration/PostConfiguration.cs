using CMS.Core.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Configurations
{
    /// <summary>
    /// Entity configuration for the Post domain model using Fluent API.
    /// Defines how the Post entity maps to the underlying relational database.
    /// </summary>
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // 1. Map the entity to the "Posts" table in the database
            builder.ToTable("Posts");

            // 2. Define the Primary Key
            builder.HasKey(x => x.Id);

            // 3. Configure the Name property: Max length 250 and NOT NULL
            builder.Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            // 4. Configure the Slug property: Use 'varchar' for performance (standard for URLs)
            // Marked as Required (NOT NULL)
            builder.Property(x => x.Slug)
                .HasColumnType("varchar(250)")
                .IsRequired();

            // 5. Create a Unique Index for the Slug column
            // Ensures SEO-friendly URLs are unique and optimized for fast lookups
            builder.HasIndex(x => x.Slug)
                .IsUnique();

            // 6. Configure optional metadata fields with specific length constraints
            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.SeoDescription)
                .HasMaxLength(160);

            // 7. Configure monetary value: 18 total digits with 2 decimal places
            // Prevents precision loss for financial calculations (royalties)
            builder.Property(x => x.RoyaltyAmount)
                .HasColumnType("decimal(18,2)");

            // 8. CONFIGURE ENUM CONVERSION (STATUS)
            // Stores Enum values as readable Strings in the DB instead of Integers
            builder.Property(x => x.Status)
                .HasMaxLength(50)
                .HasConversion(
                    v => v.ToString(), // Convert Enum to String when saving to DB
                    v => (PostStatus)Enum.Parse(typeof(PostStatus), v)) // Parse String back to Enum when reading
                .HasDefaultValue(PostStatus.Draft); // Default status for new posts

            // 9. CONFIGURE RELATIONSHIPS
            // Defined as: One Post belongs to One Category (1-to-Many relationship)
            builder.HasOne(x => x.Category)
                .WithMany()                      // A category can contain multiple posts
                .HasForeignKey(x => x.CategoryId) // Mapping foreign key property
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete to protect data integrity
        }
    }
}