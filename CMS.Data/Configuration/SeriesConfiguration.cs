using CMS.Core.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Configurations
{
    /// <summary>
    /// Database configuration for the Series entity using Fluent API.
    /// </summary>
    public class SeriesConfiguration : IEntityTypeConfiguration<Series>
    {
        public void Configure(EntityTypeBuilder<Series> builder)
        {
            // 1. Table mapping
            builder.ToTable("Series");

            // 2. Primary Key
            builder.HasKey(x => x.Id);

            // 3. Name configuration
            builder.Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            // 4. Slug configuration: varchar for performance + Unique Index
            builder.Property(x => x.Slug)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.HasIndex(x => x.Slug)
                .IsUnique();

            // 5. Metadata and UI configuration
            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.Thumbnail)
                .HasMaxLength(500);

            builder.Property(x => x.SeoDescription)
                .HasMaxLength(160);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.SortOrder)
                .HasDefaultValue(0);

            // 6. Relationship configuration
            // Series has many PostInSeries entries
            builder.HasMany(x => x.PostInSeries)
                .WithOne(x => x.Series)
                .HasForeignKey(x => x.SeriesId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}