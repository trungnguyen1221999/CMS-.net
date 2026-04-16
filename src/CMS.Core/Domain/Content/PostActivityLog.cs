using CMS.Core.Domain.Identity;

namespace CMS.Core.Domain.Content
{
    /// <summary>
    /// Represents a log entry for tracking post status transitions.
    /// Used for auditing the publishing workflow history.
    /// </summary>
    public class PostActivityLog
    {
        // Primary Key
        public Guid Id { get; set; }

        // Reference to the post being modified
        public Guid PostId { get; set; }

        // The status before the change
        public PostStatus FromStatus { get; set; }

        // The status after the change
        public PostStatus ToStatus { get; set; }

        // Timestamp of the activity
        public DateTime DateCreated { get; set; }

        // Optional comment or reason for the status change
        public string? Note { get; set; }

        // The User ID of the person who performed this action
        public Guid UserId { get; set; }

        // --- Navigation Properties ---

        // Linking back to the Post for easier querying from the log
        public virtual Post Post { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;
    }
}