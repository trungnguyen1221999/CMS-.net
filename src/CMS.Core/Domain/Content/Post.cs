using CMS.Core.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Domain.Content;

public class Post
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Slug { get; set; }

    public string? Description { get; set; }

    public Guid CategoryId { get; set; }

    public string? Thumbnail { get; set; }

    public string? Content { get; set; }

    // ID của Tác giả (Bắt buộc)
    public Guid UserId { get; set; }

    public string? Source { get; set; }

    public string? Tags { get; set; }

    public string? SeoDescription { get; set; }

    public int ViewCount { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool IsPaid { get; set; }

    public decimal RoyaltyAmount { get; set; }

    public PostStatus Status { get; set; }

    // ID người duyệt (Có thể null khi mới tạo nháp)
    public Guid? ApproverUserId { get; set; }

    // --- Navigation Properties ---

    public virtual PostCategory Category { get; set; } = null!;

    // Cùng trỏ về AppUser nhưng đóng vai trò khác nhau
    public virtual AppUser Author { get; set; } = null!;
    public virtual AppUser? Approver { get; set; }
}

public enum PostStatus
{
    Draft = 1,
    WaitingForApproval = 2,
    WaitingForPublishing = 3,
    Published = 4,
    Rejected = 5,
    Cancelled = 6
}