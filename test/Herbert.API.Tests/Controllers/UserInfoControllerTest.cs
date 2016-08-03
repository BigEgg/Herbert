namespace Herbert.API.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    using Herbert.API.Controllers;
    using Herbert.API.ViewModels.UserInfo;
    using Herbert.Services.UserInfo;

    public class UserInfoControllerTest
    {
        private Mock<IApplicationUserService> applicationUserService = new Mock<IApplicationUserService>();
        
        [Fact(DisplayName = "Should return is email used result")]
        public void TestCheckEmail()
        {
            var controller = new UserInfoController(applicationUserService.Object);
            applicationUserService.SetupSequence(service => service.IsEmailAlreadyUsed("abc@bigegg.com"))
                                  .Returns(true)
                                  .Returns(false);

            var result = controller.CheckEmail(new CheckEmailRequest()
            {
                Email = "abc@bigegg.com"
            });
            Assert.IsType(typeof(OkObjectResult), result);
            var data = (result as OkObjectResult).Value as CheckEmailResponse;
            Assert.True(data.IsUsed);

            result = controller.CheckEmail(new CheckEmailRequest()
            {
                Email = "abc@bigegg.com"
            });
            Assert.IsType(typeof(OkObjectResult), result);
            data = (result as OkObjectResult).Value as CheckEmailResponse;
            Assert.False(data.IsUsed);
        }
    }
}
