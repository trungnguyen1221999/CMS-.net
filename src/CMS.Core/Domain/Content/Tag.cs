namespace CMS.Core.Domain.Content
{
    /// <summary>
    /// Represents a keyword or label used to categorize and filter posts.
    /// Provides a flexible way to organize content across different categories.
    /// </summary>
    public class Tag
    {
        // Primary Key for the tag
        public Guid Id { get; set; }

        // The display name of the tag (e.g., "React", "DotNet", "Tutorial")
        public required string Name { get; set; }

        // --- Navigation Properties ---

        // Collection of link entries connecting this tag to various posts
        public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}