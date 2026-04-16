using CMS.Core.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers"); // Nên dùng số nhiều cho bảng
            builder.HasKey(x => x.Id);

            // Cấu hình các trường bạn thêm vào
            builder.Property(x => x.FirstName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Avatar).HasMaxLength(500);
            builder.Property(x => x.Balance).HasColumnType("decimal(18,2)");
            builder.Property(x => x.RoyaltyAmountPerPost).HasColumnType("decimal(18,2)");

            // Cấu hình lại một số trường của Identity (Tùy chọn nhưng nên làm)
            builder.Property(u => u.UserName).HasMaxLength(250).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(250).IsRequired();
            builder.Property(u => u.NormalizedUserName).HasMaxLength(250);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(250);
        }
    }
}
