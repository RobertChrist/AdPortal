namespace AdPortal.Domain
{
    /// <summary>
    /// A view model for display advertisement data to the user.
    /// </summary>
    public class PaginatedAdListViewModel : AdListViewModel
    {
        /// <summary>
        /// The index of the first Display Ad in the total result set of the initial query.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// How many pages it will take to display all of the ads returned in the initial query.
        /// </summary>
        public int TotalPageCount { get; set; }
    }
}
