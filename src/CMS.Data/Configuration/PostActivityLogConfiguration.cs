using CMS.Core.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Configurations
{
    /// <summary>
    /// Database configuration for PostActivityLog using Fluent API.
    /// </summary>
    public class PostActivityLogConfiguration : IEntityTypeConfiguration<PostActivityLog>
    {
        public void Configure(EntityTypeBuilder<PostActivityLog> builder)
        {
            // 1. Map to table name
            builder.ToTable("PostActivityLogs");

            // 2. Set Primary Key
            builder.HasKey(x => x.Id);

            // 3. Configure properties
            builder.Property(x => x.Note)
                .HasMaxLength(500); // Reason for status change (optional)

            builder.Property(x => x.DateCreated)
                .IsRequired();

            // 4. Configure Enum conversions for statuses
            // Storing as strings in DB for better traceability in audit logs
            builder.Property(x => x.FromStatus)
                .HasMaxLength(50)
                .HasConversion(
                    v => v.ToString(),
                    v => (PostStatus)Enum.Parse(typeof(PostStatus), v));

            builder.Property(x => x.ToStatus)
                .HasMaxLength(50)
                .HasConversion(
                    v => v.ToString(),
                    v => (PostStatus)Enum.Parse(typeof(PostStatus), v));

            // 5. Configure Relationship (1 Post has Many Activity Logs)
            builder.HasOne(x => x.Post)
                .WithMany() // Or .WithMany(p => p.ActivityLogs) if you add a collection in Post entity
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade); // If a post is deleted, its logs are also removed

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}