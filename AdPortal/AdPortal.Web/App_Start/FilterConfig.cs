namespace AdPortal.Web
{
    using System.Web.Mvc;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, object[] customFilters)
        {
            filters.Add(new HandleErrorAttribute());
            foreach (object filter in customFilters)
            {
                filters.Add(filter);
            }
        }
    }
}