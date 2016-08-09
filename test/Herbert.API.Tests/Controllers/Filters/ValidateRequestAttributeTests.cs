namespace Herbert.API.Tests.Controllers.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    using Herbert.API.Controllers.Filters;

    public class ValidateRequestAttributeTests : FilterTestBase
    {
        [Fact(DisplayName = "Should do nothing when model is valid")]
        public void TestOnActionExecuting_Valid()
        {
            SetUp();

            var filter = new ValidateRequestAttribute();
            filter.OnActionExecuting(actionContext);

            Assert.Null(actionContext.Result);
        }

        [Fact(DisplayName = "Should set result as BadRequest when model is invalid")]
        public void TestOnActionExecuting_InValid()
        {
            SetUp();
            modelState.AddModelError("key", "some error");

            var filter = new ValidateRequestAttribute();
            filter.OnActionExecuting(actionContext);

            Assert.NotNull(actionContext.Result);
            Assert.IsType(typeof(BadRequestObjectResult), actionContext.Result);
        }
    }
}
