namespace AdPortal.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This class is used to perform CRUD operations on Ad data.
    /// </summary>
    public class AdRepository
    {
        private readonly IDataService _dataService;

        public AdRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Return all ad data within a given date range.
        /// </summary>
        /// <param name="startDate">The beginning of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <param name="sortBy">A string name of parameter by which to sort the results.</param>
        /// <param name="lengthPerPage">How many ads will be returned (maximum).</param>
        /// <param name="page">After ordering the result, top offset by this number of items.</param>
        /// <returns>Ad data.</returns>
        public PaginatedAdListViewModel GetAllAds(DateTime startDate, DateTime endDate, 
            string sortBy, bool ascending, int lengthPerPage, int page)
        {
            Ad[] rawAds = _dataService.GetAdDataByDateRange(startDate, endDate);

            List<Ad> adsList = rawAds == null ? new List<Ad>() : rawAds.ToList();
            var sortedAds = DynamicOrderBy(adsList, sortBy, ascending);

            var finalAdList = sortedAds.Skip(page * lengthPerPage).Take(lengthPerPage);

            return new PaginatedAdListViewModel
                       {
                           Page = page,
                           DisplayAds = finalAdList,
                           TotalPageCount = (int)Math.Ceiling((double)(sortedAds.Count()) / (double)lengthPerPage)
                       };
        }

        /// <summary>
        /// Return all ad data for ads of type position "Cover".
        /// </summary>
        /// <param name="startDate">The beginning of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <param name="sortBy">A string name of parameter by which to sort the results.</param>
        /// <param name="ascending">boolean for whether list should be sorted in ascending order.</param>
        /// <param name="lengthPerPage">How many ads will be returned (maximum).</param>
        /// <param name="page">After ordering the result, top offset by this number of items.</param>
        /// <returns>Ad data</returns>
        public PaginatedAdListViewModel GetCoverAds(DateTime startDate, DateTime endDate, 
            string sortBy, bool ascending, int lengthPerPage, int page)
        {
            Ad[] rawAds = _dataService.GetAdDataByDateRange(startDate, endDate);
            List<Ad> adsList = rawAds == null ? new List<Ad>() : rawAds.ToList();

            IEnumerable<Ad> filteredList = adsList.Where(ad => string.Equals(ad.Position, "Cover", StringComparison.OrdinalIgnoreCase) 
                && decimal.Compare(ad.NumPages, .5m) > 0);
            var sortedList = DynamicOrderBy(filteredList, sortBy, ascending);
            var totalCount = sortedList.Count();
            var finalAdList = sortedList.Skip(page * lengthPerPage).Take(lengthPerPage);

            return new PaginatedAdListViewModel
            {
                DisplayAds = finalAdList,
                TotalPageCount = (int)Math.Ceiling((double)totalCount / (double)lengthPerPage),
                Page = page
            };
        }

        /// <summary>
        /// Get the top advertisements, by coverage amount, distinct by brand.
        /// </summary>
        /// <param name="startDate">The beginning of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <param name="maxLength">How many ads will be returned (maximum).</param>
        /// <returns> the top advertisements, by coverage amount, distinct by brand.</returns>
        public AdListViewModel GetTopAds(DateTime startDate, DateTime endDate, int maxLength)
        {
            Ad[] rawAds = _dataService.GetAdDataByDateRange(startDate, endDate);
            List<Ad> adsList = rawAds == null ? new List<Ad>() : rawAds.ToList();

            var temp = adsList.GroupBy(ad => ad.Brand.BrandId).Select(
                group =>
                {
                    decimal maxPrice = group.Max(x => x.NumPages);
                    return group.First(x => x.NumPages == maxPrice);
                }).OrderByDescending(ad => ad.NumPages).ThenBy(ad => ad.Brand.BrandName)
                    .Take(maxLength);

            return new AdListViewModel() { DisplayAds = temp };
        }

        /// <summary>
        /// Get the top brands by coverage amount of all ads.
        /// </summary>
        /// <param name="startDate">The beginning of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <param name="maxLength">How many ads will be returned (maximum).</param>
        /// <returns>top brands by coverage amount of all ads.</returns>
        public IEnumerable<TopBrandViewModel> GetTopBrands(DateTime startDate, DateTime endDate, int maxLength)
        {
            Ad[] rawAds = _dataService.GetAdDataByDateRange(startDate, endDate);

            List<Ad> adsList = rawAds == null ? new List<Ad>() : rawAds.ToList();

            var brandCoverage = from ad in adsList
                                group ad by new { ad.Brand.BrandName } into g
                         select new TopBrandViewModel { BrandName = g.Key.BrandName, TotalPageCoverage = g.Sum(s => s.NumPages) };

            return brandCoverage.OrderByDescending(group => group.TotalPageCoverage)
                  .ThenBy(group => group.BrandName)
                  .Take(maxLength);
        }
        
        private IEnumerable<Ad> DynamicOrderBy(IEnumerable<Ad> data, string sortPropertyName, bool ascending)
        {
            if (ascending)
            {
                return data.OrderBy(x => GetPropertyValue(x, sortPropertyName));
            }

            return data.OrderByDescending(x => GetPropertyValue(x, sortPropertyName));
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            try
            {
                foreach (var prop in propertyName.Split('.').Select(s => obj.GetType().GetProperty(s)))
                {
                    obj = prop.GetValue(obj, null);
                }
                return obj;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}
