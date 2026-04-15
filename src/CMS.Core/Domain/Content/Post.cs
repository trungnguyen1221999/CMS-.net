namespace CMS.Core.Domain.Content
{
    /// <summary>
    /// Represents a blog post or article in the system.
    /// This is the core Domain Entity for content management.
    /// </summary>
    public class Post
    {
        // Primary Key using Globally Unique Identifier (GUID)
        public Guid Id { get; set; }

        // The display title of the post
        public required string Name { get; set; }

        // URL-friendly version of the name (e.g., "my-first-post")
        public required string Slug { get; set; }

        // Short summary used for SEO or post previews
        public string? Description { get; set; }

        // Foreign Key to the PostCategory table
        public Guid CategoryId { get; set; }

        // URL or path to the post's featured image
        public string? Thumbnail { get; set; }

        // The main body content (usually HTML from a Rich Text Editor)
        public string? Content { get; set; }

        // Foreign Key to the User (Author) who created the post
        public Guid UserId { get; set; }

        // Reference link if the content is curated from another site
        public string? Source { get; set; }

        // Comma-separated tags or JSON string for categorization
        public string? Tags { get; set; }

        // Specific description optimized for Search Engines (meta description)
        public string? SeoDescription { get; set; }

        // Total number of times the post has been viewed
        public int ViewCount { get; set; }

        // Audit field: Timestamp when the post was first created
        public DateTime DateCreated { get; set; }

        // Audit field: Timestamp for the last modification (null if never updated)
        public DateTime? DateUpdated { get; set; }

        // Flag indicating if the author has been paid for this post
        public bool IsPaid { get; set; }

        // The amount of money to be paid to the author for this post
        public decimal RoyaltyAmount { get; set; }

        // Current workflow state of the post
        public PostStatus Status { get; set; }

        /// <summary>
        /// Foreign Key to the User who approved the post.
        /// Nullable because a draft or pending post does not have an approver yet.
        /// </summary>
        public Guid? ApproverUserId { get; set; }

        // --- Navigation Properties ---

        /// <summary>
        /// Relational mapping to the Category object.
        /// Initialized as null! to satisfy nullable reference types while allowing EF Core to populate it.
        /// </summary>
        public virtual PostCategory Category { get; set; } = null!;
    }

    /// <summary>
    /// Defines the possible states of a post in the publishing workflow.
    /// </summary>
    public enum PostStatus
    {
        Draft = 1,                 // Work in progress, only visible to the author
        WaitingForApproval = 2,    // Submitted for review by an editor/admin
        WaitingForPublishing = 3,  // Approved but scheduled for a future date
        Published = 4,             // Live on the website for public view
        Rejected = 5,              // Sent back to the author for revisions
        Cancelled = 6              // Withdrawn or deleted from the workflow
    }
}