namespace AdPortal.Web
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using AdPortal.Web.Filters;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static ILogger GlobalLogger;

        protected void Application_Start()
        {
            // AreaRegistration.RegisterAllAreas();
            // WebApiConfig.Register(GlobalConfiguration.Configuration);
            // BundleConfig.RegisterBundles(BundleTable.Bundles);
            // AuthConfig.RegisterAuth();

            // Alternatively, we may want to hold a reference to this in AdPortal.dll, 
            // so that we can be certain that all projects in this solution
            // future and current use the same logging implementation.
            GlobalLogger = new MediaRadarLoggerMock();
            var unhandledErrorAttr = new LoggingRoutingHandleErrorAttribute(GlobalLogger);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, new object[] { unhandledErrorAttr });
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new AdPortalControllerFactory());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception exception = Server.GetLastError();
                GlobalLogger.Error(exception.Message, exception);

                var statusCode = 500;
                HttpException httpException = exception as HttpException;
                if (httpException != null)
                    statusCode = httpException.GetHttpCode();

                var errorMessage = GlobalErrorMessages.HTTP_500;
                if (statusCode == 404)
                    errorMessage = GlobalErrorMessages.HTTP_404;

                //Prepare new view result
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "Error");
                routeData.Values.Add("errorMessage", errorMessage);
                routeData.Values.Add("statusCode", statusCode);

                var requestContext = new RequestContext(
                    new HttpContextWrapper(HttpContext.Current), routeData);

                var controller = ControllerBuilder.Current.GetControllerFactory().CreateController(requestContext, "Error");
                controller.Execute(requestContext);

                HttpContext.Current.Server.ClearError();
            }
            catch (Exception ex)
            {
                //This should never occur, but just in case.
                GlobalLogger.Fatal(ex.Message, ex);
            }
        }
    }
}