namespace AdPortal.Tests.Filters
{
    using System;
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using AdPortal;
    using AdPortal.Web.Filters;
    using System.Web;
    using System.Web.Routing;

    [TestClass]
    public class LoggingHandleErrorAttributeTests
    {
        private Mock<HttpResponseBase> _responseMock;
        private ExceptionContext _exceptionContext;

        [TestInitialize]
        public void BuildFakeExceptionContext()
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.HttpMethod).Returns("GET");

            _responseMock = new Mock<HttpResponseBase>();

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.Request).Returns(request.Object);
            mockHttpContext.Setup(c => c.Response).Returns(_responseMock.Object);

            var mockRouteData = new RouteData();
            mockRouteData.Values.Add("key1", "value1");

            var fakeControllerContext = new ControllerContext(mockHttpContext.Object, mockRouteData, 
                new Mock<ControllerBase>().Object);

            var ex = new Exception("Example Unhandled Controller Exception");
            _exceptionContext = new ExceptionContext(fakeControllerContext, ex);
        }

        [TestMethod]
        public void LoggingFilter_ExceptionIsAlreadyHandled_ExceptionIsNotHandledAgain()
        {
            _exceptionContext.ExceptionHandled = true;

            Mock<ILogger> logger = new Mock<ILogger>();
            var target = new LoggingRoutingHandleErrorAttribute(logger.Object);

            target.OnException(_exceptionContext);

            // Exception is not un-handled or logged
            Assert.IsTrue(_exceptionContext.ExceptionHandled);
            logger.Verify(l => l.Error(_exceptionContext.Exception.Message, _exceptionContext.Exception), 
                Times.Exactly(0), "Logger was called despite not handling the error.");
        }

        [TestMethod]
        public void LoggingFilter_ExceptionIsAlreadyHandled_ErrorHttpResponseIsNotSet()
        {
            _exceptionContext.ExceptionHandled = true;

            Mock<ILogger> logger = new Mock<ILogger>();
            var target = new LoggingRoutingHandleErrorAttribute(logger.Object);

            target.OnException(_exceptionContext);

            // Http Response is not set to display an error.
            Assert.IsNull(_exceptionContext.Result as ViewResult);
            _responseMock.VerifySet(x => x.StatusCode = 500, Times.Never);
        }

        [TestMethod]
        public void LoggingFilter_NullLogger_HandlesException()
        {
            var target = new LoggingRoutingHandleErrorAttribute(null);

            target.OnException(_exceptionContext);

            Assert.IsTrue(_exceptionContext.ExceptionHandled);
            // No exception thrown attempting to log
            
        }

        [TestMethod]
        public void LoggingFilter_NullLogger_SetsHttpResponse()
        {
            var target = new LoggingRoutingHandleErrorAttribute(null);

            target.OnException(_exceptionContext);

            _responseMock.VerifySet(x => x.StatusCode = 500, Times.Once);
            Assert.AreEqual("Error", ((ViewResult)_exceptionContext.Result).ViewName);
        }

        [TestMethod]
        public void LoggingFilter_Normal_HandlesException()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            var target = new LoggingRoutingHandleErrorAttribute(logger.Object);
            
            target.OnException(_exceptionContext);

            Assert.IsTrue(_exceptionContext.ExceptionHandled);
            logger.Verify(l => l.Error(_exceptionContext.Exception.Message, _exceptionContext.Exception), 
                Times.Exactly(1), "Logger was not called with the unhandled exception.");
        }

        [TestMethod]
        public void LoggingFilter_Normal_SetsHttpResponse()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            var target = new LoggingRoutingHandleErrorAttribute(logger.Object);

            target.OnException(_exceptionContext);

            Assert.AreEqual("Error", ((ViewResult)_exceptionContext.Result).ViewName);
            _responseMock.VerifySet(x => x.StatusCode = 500, Times.Once);
        }
    }
}
