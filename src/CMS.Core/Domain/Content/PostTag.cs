namespace CMS.Core.Domain.Content
{
    /// <summary>
    /// Join entity representing the Many-to-Many relationship between a Post and a Tag.
    /// Acts as a link to associate multiple tags with multiple posts.
    /// </summary>
    public class PostTag
    {
        // Reference to the associated Post
        public Guid PostId { get; set; }

        // Reference to the associated Tag
        public Guid TagId { get; set; }

        // --- Navigation Properties ---

        // The Post entity associated with this tag link
        public virtual Post Post { get; set; } = null!;

        // The Tag entity associated with this post link
        public virtual Tag Tag { get; set; } = null!;
    }
}