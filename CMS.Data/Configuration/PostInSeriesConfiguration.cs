using CMS.Core.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Configurations
{
    /// <summary>
    /// Database configuration for PostInSeries using Fluent API.
    /// Configures the Composite Primary Key and Foreign Key constraints.
    /// </summary>
    public class PostInSeriesConfiguration : IEntityTypeConfiguration<PostInSeries>
    {
        public void Configure(EntityTypeBuilder<PostInSeries> builder)
        {
            // 1. Map to table name
            builder.ToTable("PostInSeries");

            // 2. Define Composite Primary Key (PostId + SeriesId)
            // This ensures a post cannot be added to the same series more than once
            builder.HasKey(x => new { x.PostId, x.SeriesId });

            // 3. Configure DisplayOrder
            builder.Property(x => x.DisplayOrder)
                .IsRequired()
                .HasDefaultValue(0);

            // 4. Configure Relationships

            // Link to Post: One Post can be in many Series entries
            builder.HasOne(x => x.Post)
                .WithMany() // Or .WithMany(p => p.PostInSeries) if defined in Post
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Link to Series: One Series contains many Post entries
            builder.HasOne(x => x.Series)
                .WithMany() // Or .WithMany(s => s.PostInSeries) if defined in Series
                .HasForeignKey(x => x.SeriesId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}