namespace AdPortal.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using AdPortal.Domain;

    public class HomeController : Controller
    {
        private readonly DateTime START_DATE = new DateTime(2011, 1, 1);
        private readonly DateTime END_DATE = new DateTime(2011, 4, 1);

        private const int ITEMS_PER_PAGE = 50;

        private readonly AdRepository _repo;

        public HomeController(AdRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index(string sortBy = "Brand.BrandName", bool ascending = true, int page = 1)
        {
            page--; // user's page numbers start at 1.

            var vm = _repo.GetAllAds(START_DATE, END_DATE, sortBy, ascending, ITEMS_PER_PAGE, page);
            
            return View(vm);
        }

        public ActionResult CoverAds(string sortBy = "Brand.BrandName", bool ascending = true, int page = 1)
        {
            page--; // user's page numbers start at 1.

            var vm = _repo.GetCoverAds(START_DATE, END_DATE, sortBy, ascending, ITEMS_PER_PAGE, page);

            return View("Index", vm);
        }

        public ActionResult TopAds()
        {
            const int NUM_ADS_TO_RETURN = 5;

            var vm = _repo.GetTopAds(START_DATE, END_DATE, NUM_ADS_TO_RETURN);

            return View(vm);
        }

        public ActionResult TopBrands()
        {
            const int NUM_ADS_TO_RETURN = 5;

            var vm = _repo.GetTopBrands(START_DATE, END_DATE, NUM_ADS_TO_RETURN);

            return View(vm);
        }
    }
}
