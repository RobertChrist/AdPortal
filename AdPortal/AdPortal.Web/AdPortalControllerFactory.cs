namespace AdPortal.Web
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using AdPortal.Web.Controllers;

    public class AdPortalControllerFactory : DefaultControllerFactory
    {
        // Our DI config

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == typeof(HomeController))
            {
                var dataService = new AdPortal.DAL.DataService();
                var repo = new AdPortal.Domain.AdRepository(dataService);
                return new HomeController(repo);
            }

            if (controllerType == typeof(JSTestRunnerController))
            { 
#if DEBUG
                return new JSTestRunnerController();
#endif
                throw new HttpException(404, "");
            }
        
            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}