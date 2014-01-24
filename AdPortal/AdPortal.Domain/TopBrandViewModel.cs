namespace AdPortal.Domain
{
    /// <summary>
    /// A view model for displaying top brands to the user.
    /// </summary>
    public class TopBrandViewModel
    {
        /// <summary>
        /// The name for the brand.
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// The total sum of that brands page coverage across all results for the initial query.
        /// </summary>
        public decimal TotalPageCoverage { get; set; }
    }
}
