namespace CMS.Core.Domain.Content
{
    /// <summary>
    /// Represents a category for grouping posts. 
    /// Supports hierarchical structures (Parent-Child relationships).
    /// </summary>
    public class PostCategory
    {
        // Primary Key
        public Guid Id { get; set; }

        // The display name of the category (e.g., "Software Development")
        public required string Name { get; set; }

        // URL-friendly version of the name (e.g., "software-development")
        public required string Slug { get; set; }

        // Reference to a parent category for nested structures (null if top-level)
        public Guid? ParentId { get; set; }

        // Toggle to enable or disable the category on the frontend
        public bool IsActive { get; set; }

        // Audit field: Timestamp for creation
        public DateTime DateCreated { get; set; }

        // Audit field: Timestamp for the last modification
        public DateTime? DateModified { get; set; }

        // Description optimized for Search Engine results
        public string? SeoDescription { get; set; }

        // Defines the display sequence in menus or lists
        public int SortOrder { get; set; }

        // --- Navigation Properties ---

        // Self-referencing relationship to handle the parent category object
        public virtual PostCategory? Parent { get; set; }

        // Collection of sub-categories under this category
        public virtual ICollection<PostCategory> Children { get; set; } = new List<PostCategory>();
    }
}