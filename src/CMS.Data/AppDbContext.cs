using CMS.Core.Domain.Content;
using CMS.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace CMS.Data;

// Kế thừa IdentityDbContext để có sẵn các bảng User/Role bảo mật

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)

    {

    }

    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<PostActivityLog> PostActivityLogs { get; set; } = null!;
    public DbSet<PostCategory> PostCategories { get; set; } = null!;

    public DbSet<PostInSeries> PostInSeries { get; set; } = null!;
    public DbSet<PostTag> PostTags { get; set; } = null!;
    public DbSet<Series> Series { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)

    {

        // Rất quan trọng: Phải gọi base để Identity hoạt động đúng

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
        builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
            .HasKey(x => new { x.UserId, x.RoleId });
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
        builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry> entries = ChangeTracker
           .Entries()
           .Where(e => e.State == EntityState.Added);

        foreach (EntityEntry entityEntry in entries)
        {
            PropertyInfo? modifiedProp = entityEntry.Entity.GetType().GetProperty("DateUpdated");
            modifiedProp?.SetValue(entityEntry.Entity, DateTime.Now);

            if (entityEntry.State == EntityState.Added)
            {
                PropertyInfo? createdProp = entityEntry.Entity.GetType().GetProperty("DateCreated");
                createdProp?.SetValue(entityEntry.Entity, DateTime.Now);
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}