namespace AdPortal.Web.Filters
{
    using System.Web.Mvc;

    using AdPortal;

    // WARNING: A shortcoming for the handleerrorattribute incorrectly handles failed download requests
    // on failed download requests.  See http://perspectivespace.com/error-handling-in-aspnet-mvc-3-part-3-handlee
    // As such, errors must also be handled in the global.asax error method.
    
    /// <summary>
    /// Extends the traditional HandleErrorAttribute to log the exception, and reroute the user
    /// to an error page when intercepting unhandled exceptions.
    /// </summary>
    public class LoggingRoutingHandleErrorAttribute : HandleErrorAttribute
    {
        private readonly ILogger _logger;

        public LoggingRoutingHandleErrorAttribute(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Is responsible for handling errors that bubble through the controller, with some limitations.
        /// If and only if the exception is not already handled, this method will handle the error
        /// by setting the viewResult to a View with the name Error, setting an ErrorMessage in the Viewbag, 
        /// and setting the response status code.  If a logger was supplied in the constructor, then the error 
        /// will be logged to the error function in the logger.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            if (!filterContext.ExceptionHandled)
            {
                if (_logger != null)
                {
                    _logger.Error(filterContext.Exception.Message, filterContext.Exception);
                }

                ViewResult viewResult = new ViewResult { ViewName = "Error" };

                // Normally I use a logging framework that allows me to display an ID to the user, 
                // so that support can identify which error the user sees and query the backend.
                // For this project we defer to MediaRadar's logging implementation mocked out in AdPortal.dll.
                // viewResult.ViewBag.CorrelationId = logID; 

                viewResult.ViewBag.ErrorMessage = GlobalErrorMessages.HTTP_500;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = viewResult;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}