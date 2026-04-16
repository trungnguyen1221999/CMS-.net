using CMS.Core.Domain.Content;
using CMS.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace CMS.Data;



// Kế thừa IdentityDbContext để có sẵn các bảng User/Role bảo mật

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>

{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)

    {

    }

    // 1. Khai báo các "Thực thể" muốn biến thành Bảng trong SQL

    public DbSet<Post> Posts { get; set; }

    public DbSet<PostCategory> PostCategories { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)

    {

        // Rất quan trọng: Phải gọi base để Identity hoạt động đúng

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // (Mẹo) Nếu bạn muốn chắc chắn Identity không tự tạo bảng mặc định:
        builder.Entity<AppUser>(entity => { entity.ToTable(name: "AppUsers"); });
        builder.Entity<AppRole>(entity => { entity.ToTable(name: "AppRoles"); });
    }

}