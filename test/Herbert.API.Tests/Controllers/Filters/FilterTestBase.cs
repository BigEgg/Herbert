﻿namespace Herbert.API.Tests.Controllers.Filters
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using Moq;
    using System.Collections.Generic;

    public abstract class FilterTestBase
    {
        protected readonly Mock<HttpRequest> mockHttpRequest = new Mock<HttpRequest>();
        protected readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        protected readonly Mock<RouteData> mockRouteData = new Mock<RouteData>();
        protected readonly Mock<ControllerActionDescriptor> mockActionDescriptor = new Mock<ControllerActionDescriptor>();
        protected readonly HeaderDictionary headers = new HeaderDictionary();
        protected readonly ModelStateDictionary modelState = new ModelStateDictionary();
        protected readonly Controller controller = new FakeController();

        protected ActionContext context;
        protected ActionExecutingContext actionContext;

        protected void SetUp()
        {
            mockHttpRequest.Reset();
            mockHttpContext.Reset();
            mockRouteData.Reset();
            mockActionDescriptor.Reset();
            modelState.Clear();
            headers.Clear();

            mockHttpContext.Setup(context => context.Request).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(request => request.Headers).Returns(headers);

            context = new ActionContext(mockHttpContext.Object, mockRouteData.Object, mockActionDescriptor.Object, modelState);
            actionContext = new ActionExecutingContext(context, new List<IFilterMetadata>(), new Dictionary<string, object>(), controller);
            controller.ControllerContext = new ControllerContext(context);
        }
    }
}
