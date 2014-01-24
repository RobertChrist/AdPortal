namespace AdPortal.Domain
{
    /// <summary>
    /// A model that represents a company, organization or person that advertises.
    /// </summary>
    public class Brand
    {
        /// <summary>
        /// The brand's unique identifier.
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// The display name of the brand.
        /// </summary>
        public string BrandName { get; set; }
    }
}
