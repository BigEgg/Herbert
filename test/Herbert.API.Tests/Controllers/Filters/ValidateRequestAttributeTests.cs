namespace Herbert.API.Tests.Controllers.Filters
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using Moq;
    using System.Collections.Generic;
    using Xunit;

    using Herbert.API.Controllers.Filters;

    public class ValidateRequestAttributeTests
    {
        [Fact(DisplayName = "Should do nothing when model is valid")]
        public void TestOnActionExecuting_Valid()
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockRouteData = new Mock<RouteData>();
            var mockActionDescriptor = new Mock<ControllerActionDescriptor>();
            var mockModelState = new ModelStateDictionary();

            var context = new ActionContext(mockHttpContext.Object, mockRouteData.Object, mockActionDescriptor.Object, mockModelState);
            var controller = new FakeController();

            var actionContext = new ActionExecutingContext(context, new List<IFilterMetadata>(), new Dictionary<string, object>(), controller);
            controller.ControllerContext = new ControllerContext(context);

            var filter = new ValidateRequestAttribute();
            filter.OnActionExecuting(actionContext);

            Assert.Null(actionContext.Result);
        }

        [Fact(DisplayName = "Should set result as BadRequest when model is invalid")]
        public void TestOnActionExecuting_InValid()
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockRouteData = new Mock<RouteData>();
            var mockActionDescriptor = new Mock<ControllerActionDescriptor>();
            var mockModelState = new ModelStateDictionary();

            var context = new ActionContext(mockHttpContext.Object, mockRouteData.Object, mockActionDescriptor.Object, mockModelState);
            var controller = new FakeController();

            var actionContext = new ActionExecutingContext(context, new List<IFilterMetadata>(), new Dictionary<string, object>(), controller);
            controller.ControllerContext = new ControllerContext(context);
            mockModelState.AddModelError("key", "some error");

            var filter = new ValidateRequestAttribute();
            filter.OnActionExecuting(actionContext);

            Assert.NotNull(actionContext.Result);
            Assert.IsType(typeof(BadRequestObjectResult), actionContext.Result);
        }
    }
}
