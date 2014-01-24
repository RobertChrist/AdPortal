namespace AdPortal.DAL
{
    using System;
    using AdPortal.Domain;

    public class DataService : IDataService
    {
        public DataService()
        {
            AutoMapper.Mapper.CreateMap<AdService.Ad, AdPortal.Domain.Ad>();
            AutoMapper.Mapper.CreateMap<AdService.Brand, AdPortal.Domain.Brand>();
        }

        public Ad[] GetAdDataByDateRange(DateTime startDate, DateTime endDate)
        {
            // This would be a great location for some caching, though with my current knowledge of this service
            // I cannot currently assume that the data for this, or any date range, is actually static
            // Something as simple as checking and storing responses in 
            // System.Web.HttpContext.Current.Cache could be sufficient. 

            var service = new AdService.AdDataServiceClient();
            AdService.Ad[] rawAdData;
            try
            {
                // TODO: Maximum message size set to 2MB, talk with service provider about choosing a less arbitrary number.
                rawAdData = service.GetAdDataByDateRange(startDate, endDate);
                service.Close();
            }
            catch
            {
                service.Abort();
                throw;
            }

            return AutoMapper.Mapper.Map<AdService.Ad[], AdPortal.Domain.Ad[]>(rawAdData);
        }
    }
}
