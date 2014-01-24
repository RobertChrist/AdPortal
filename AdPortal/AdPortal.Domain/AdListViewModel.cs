namespace AdPortal.Domain
{
    using System.Collections.Generic;

    public class AdListViewModel
    {
        /// <summary>
        /// The ads to display to the user.
        /// </summary>
        public IEnumerable<Ad> DisplayAds { get; set; }
    }
}
