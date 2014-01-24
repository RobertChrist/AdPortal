namespace AdPortal.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ErrorController() { }
        
        /// <summary>
        /// Displays the custom error page.
        /// </summary>
        /// <param name="errorMessage">The error message to display to the user.</param>
        /// <returns>The error view.</returns>
        public ActionResult Error(string errorMessage, int statusCode)
        {
            // I have mocked out a VERY simple error page on the assumption that the one in this project
            // would be replaced with a standard error page
            // already in use at MediaRadar.
            
            // reset http request
            Response.ClearHeaders();
            Response.ClearContent();
            Response.StatusCode = statusCode;
            Response.TrySkipIisCustomErrors = true;

            ViewBag.ErrorMessage = errorMessage;

            return View();
        }

        // I usually include a postable method here for whenever a user hits an error page.  
        // I again assume there is already a standard available at MediaRadar.
    }
}
