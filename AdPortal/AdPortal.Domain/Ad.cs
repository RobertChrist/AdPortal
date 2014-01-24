namespace AdPortal.Domain
{
    /// <summary>
    /// A model that represents an advertisement instance.
    /// </summary>
    public class Ad
    {
        /// <summary>
        /// The advertisement's unique identifier.
        /// </summary>
        public int AdId { get; set; }

        /// <summary>
        /// The Advertisement's brand.
        /// </summary>
        public Brand Brand { get; set; }

        /// <summary>
        /// The percent of the page taken up by the advertisement.
        /// </summary>
        public decimal NumPages { get; set; }

        /// <summary>
        /// Whether the ad is a "Page" ad or "Cover" ad.
        /// </summary>
        public string Position { get; set; }
    }
}
