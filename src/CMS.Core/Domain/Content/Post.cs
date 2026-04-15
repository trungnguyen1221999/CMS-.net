using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Core.Domain.Content
{
    [Table("Posts")]
    [Index(nameof(Slug), IsUnique = true)]
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public required string Slug { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public Guid CategoryId { get; set; }

        [MaxLength(500)]
        public string? Thumbnail { get; set; }

        public string? Content { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [MaxLength(500)]
        public string? Source { get; set; }

        public string? Tags { get; set; }

        [MaxLength(160)]
        public string? SeoDescription { get; set; }

        public int ViewCount { get; set; } = 0;

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public bool IsPaid { get; set; } = false;

        [Range(0, double.MaxValue)]
        public decimal RoyaltyAmount { get; set; } = 0;

        public PostStatus Status { get; set; }
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
}