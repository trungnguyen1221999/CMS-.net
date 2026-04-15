namespace CMS.Core.Domain.Content
{
    /// <summary>
    /// Represents a series of related posts grouped together.
    /// Used for tutorials, courses, or multi-part articles.
    /// </summary>
    public class Series
    {
        // Primary Key
        public Guid Id { get; set; }

        // The display title of the series
        public required string Name { get; set; }

        // A short summary or intro to the series
        public string? Description { get; set; }

        // URL-friendly identifier (Unique)
        public required string Slug { get; set; }

        // Toggle to show/hide the series on the frontend
        public bool IsActive { get; set; }

        // Manual display order for lists or navigation
        public int SortOrder { get; set; }

        // Metadata optimized for search engine results
        public string? SeoDescription { get; set; }

        // URL or path to the cover image of the series
        public string? Thumbnail { get; set; }

        // Detailed introduction or body text for the series page
        public string? Content { get; set; }

        // The ID of the user who owns or manages this series
        public Guid OwnerUserId { get; set; }

        // --- Navigation Properties ---

        // Collection of links to posts included in this series
        public virtual ICollection<PostInSeries> PostInSeries { get; set; } = new List<PostInSeries>();
    }
}