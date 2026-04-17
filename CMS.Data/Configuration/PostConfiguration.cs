using CMS.Core.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasColumnType("varchar(250)")
            .IsRequired();

        builder.HasIndex(x => x.Slug)
            .IsUnique();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.SeoDescription)
            .HasMaxLength(160);

        builder.Property(x => x.RoyaltyAmount)
            .HasColumnType("decimal(18,2)");

        // Chuyển Enum thành String để dễ đọc trong SQL
        builder.Property(x => x.Status)
            .HasMaxLength(50)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<PostStatus>(v)) // Use generic overload
            .HasDefaultValue(PostStatus.Draft).IsRequired();

        // --- CẤU HÌNH QUAN HỆ ---

        // 1. Với Category
        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // 2. Với Author (Dùng cột UserId)
        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 3. Với Approver (Dùng cột ApproverUserId)
        builder.HasOne(x => x.Approver)
            .WithMany()
            .HasForeignKey(x => x.ApproverUserId)
            .OnDelete(DeleteBehavior.NoAction);
        
    }
}