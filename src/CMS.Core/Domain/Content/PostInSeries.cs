namespace CMS.Core.Domain.Content
{
    /// <summary>
    /// Represents the association between a Post and a Series.
    /// Acts as a join entity for the Many-to-Many relationship.
    /// </summary>
    public class PostInSeries
    {
        // Reference to the associated Post
        public Guid PostId { get; set; }

        // Reference to the associated Series
        public Guid SeriesId { get; set; }

        // The sequence number of the post within the specific series
        public int DisplayOrder { get; set; }

        // --- Navigation Properties ---

        // The Post object associated with this entry
        public virtual Post Post { get; set; } = null!;

        // The Series object associated with this entry
        public virtual Series Series { get; set; } = null!;
    }
}