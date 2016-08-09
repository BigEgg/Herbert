namespace Herbert.API.Tests.Controllers.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using Xunit;

    using Herbert.API.Controllers.Filters;
    using Herbert.Models.Access;
    using Herbert.Services.Access;

    public class ApplicationAuthorizeTest : FilterTestBase
    {
        private const string APP_AUTHORIZE_SOURCE_TYPE_KEY = "X-Herbert-ClientSource";
        private const string APP_AUTHORIZE_TOKEN_KEY = "X-Herbert-Authenticate";
        private Mock<ISupportApplicationService> mockSupportApplicationService = new Mock<ISupportApplicationService>();

        [Fact(DisplayName = "Should do nothing when pass correct authentication data")]
        public void TestOnActionExecuting_Valid()
        {
            SetUp();
            headers.Add(APP_AUTHORIZE_SOURCE_TYPE_KEY, "Website");
            headers.Add(APP_AUTHORIZE_TOKEN_KEY, "eyAiYXBwSWQiOiAiMWI0ZjczOTQtYjU3ZS00OWM3LWI1OTYtODA5ODhlYWY3MzYyIiwgImFwcFNlY3JldCI6ICJLYkZIcHk3U2p2VVBhOUxkUDlNTElCdEM1QlczV25EajlZWUdJMEZrMzVuNHIzNU9tUUpZMVZXR2FqT05HOVk2IiB9");

            mockSupportApplicationService.Setup(service => service.GetApplicationType(new Guid("1b4f7394-b57e-49c7-b596-80988eaf7362"), "KbFHpy7SjvUPa9LdP9MLIBtC5BW3WnDj9YYGI0Fk35n4r35OmQJY1VWGajONG9Y6"))
                                         .Returns(SupportApplicationType.Website);

            var filter = new ApplicationAuthorize(mockSupportApplicationService.Object);
            filter.OnActionExecuting(actionContext);

            Assert.Null(actionContext.Result);
        }

        [Theory(DisplayName = "Should set result as UnauthorizedResult when pass wrong authentication data")]
        [InlineData("Website", "")]
        [InlineData("", "eyAiYXBwSWQiOiAiMWI0ZjczOTQtYjU3ZS00OWM3LWI1OTYtODA5ODhlYWY3MzYyIiwgImFwcFNlY3JldCI6ICJLYkZIcHk3U2p2VVBhOUxkUDlNTElCdEM1QlczV25EajlZWUdJMEZrMzVuNHIzNU9tUUpZMVZXR2FqT05HOVk2IiB9")]
        [InlineData("Website123", "eyAiYXBwSWQiOiAiMWI0ZjczOTQtYjU3ZS00OWM3LWI1OTYtODA5ODhlYWY3MzYyIiwgImFwcFNlY3JldCI6ICJLYkZIcHk3U2p2VVBhOUxkUDlNTElCdEM1QlczV25EajlZWUdJMEZrMzVuNHIzNU9tUUpZMVZXR2FqT05HOVk2IiB9")]
        [InlineData("Website", "1232eyAiYXBwSWQiOiAiMWI0ZjczOTQtYjU3ZS00OWM3LWI1OTYtODA5ODhlYWY3MzYyIiwgImFwcFNlY3JldCI6ICJLYkZIcHk3U2p2VVBhOUxkUDlNTElCdEM1QlczV25EajlZWUdJMEZrMzVuNHIzNU9tUUpZMVZXR2FqT05HOVk2IiB9")]
        [InlineData("Website", "1232e^%$yAiYXBwSWQiOiAiMWI0ZjczOTQtYjU3ZS00OWM3LWI1OTYtODA5ODhlYWY3MzYyIiwgImFwcFNlY3JldCI6ICJLYkZIcHk3U2p2VVBhOUxkUDlNTElCdEM1QlczV25EajlZWUdJMEZrMzVuNHIzNU9tUUpZMVZXR2FqT05HOVk2IiB9")]
        [InlineData("Website", "eyAiaWQiOiAiMWI0ZjczOTQtYjU3ZS00OWM3LWI1OTYtODA5ODhlYWY3MzYyIiwgInNlY3JldCI6ICJLYkZIcHk3U2p2VVBhOUxkUDlNTElCdEM1QlczV25EajlZWUdJMEZrMzVuNHIzNU9tUUpZMVZXR2FqT05HOVk2IiB9")]
        [InlineData("Website", "eyAiYXBwSWQiOiAiMWI0ZjczOTQtYjU3ZS00OWM3LWI1OTYtODA5ODhlYWY3MzYyIiwgImFwcFNlY3JldCI6ICIiIH0")]
        [InlineData("Website", "eyAiYXBwSWQiOiAiIiwgImFwcFNlY3JldCI6ICJLYkZIcHk3U2p2VVBhOUxkUDlNTElCdEM1QlczV25EajlZWUdJMEZrMzVuNHIzNU9tUUpZMVZXR2FqT05HOVk2IiB9")]
        [InlineData("Website", "eyAiYXBwSWQiOiAiIiwgImFwcFNlY3JldCI6ICIiIH0")]
        [InlineData("Website", "eyAiYXBwSWQiOiAiMWI0ZjczOTQtYjU3ZS00OWM3LWI1OTYtODA5ODhlYWY3MzYyIiwgImFwcFNlY3JldCI6ICIxMjMyS2JGSHB5N1NqdlVQYTlMZFA5TUxJQnRDNUJXM1duRGo5WVlHSTBGazM1bjRyMzVPbVFKWTFWV0dhak9ORzlZNiIgfQ")]
        public void TestOnActionExecuting_Invalid(string type, string authenticateCode)
        {
            SetUp();
            if (!string.IsNullOrWhiteSpace(type))
            {
                headers.Add(APP_AUTHORIZE_SOURCE_TYPE_KEY, type);
            }
            if (!string.IsNullOrWhiteSpace(authenticateCode))
            {
                headers.Add(APP_AUTHORIZE_TOKEN_KEY, authenticateCode);
            }

            mockSupportApplicationService.Setup(service => service.GetApplicationType(new Guid("1b4f7394-b57e-49c7-b596-80988eaf7362"), "KbFHpy7SjvUPa9LdP9MLIBtC5BW3WnDj9YYGI0Fk35n4r35OmQJY1VWGajONG9Y6"))
                                         .Returns(SupportApplicationType.Website);

            var filter = new ApplicationAuthorize(mockSupportApplicationService.Object);
            filter.OnActionExecuting(actionContext);

            Assert.NotNull(actionContext.Result);
            Assert.IsType(typeof(UnauthorizedResult), actionContext.Result);
        }
    }
}
