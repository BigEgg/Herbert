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
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
            var data = (result as OkObjectResult).Value as CheckEmailResponse;
            Assert.True(data.IsUsed);

            result = controller.CheckEmail(new CheckEmailRequest()
            {
                Email = "abc@bigegg.com"
            });
            Assert.IsType(typeof(OkObjectResult), result);
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
            data = (result as OkObjectResult).Value as CheckEmailResponse;
            Assert.False(data.IsUsed);

            applicationUserService.Reset();
        }

        [Fact(DisplayName = "Should return conflict when email already been used in sign up")]
        public void TestSignUp_Exist()
        {
            var controller = new UserInfoController(applicationUserService.Object);
            applicationUserService.Setup(service => service.IsEmailAlreadyUsed("bigegg@bigegg.com"))
                                  .Returns(true);

            var result = controller.SignUp(new SignUpRequest()
            {
                Email = "bigegg@bigegg.com",
                Password = "Password!@#",
                NickName = "BigEgg",
                RegisterSource = "Website"
            });
            Assert.IsType(typeof(StatusCodeResult), result);
            Assert.Equal(409, (result as StatusCodeResult).StatusCode);

            applicationUserService.Reset();
        }

        [Fact(DisplayName = "Should success in sign up")]
        public void TestSignUp()
        {
            var controller = new UserInfoController(applicationUserService.Object);
            applicationUserService.Setup(service => service.IsEmailAlreadyUsed("bigegg@bigegg.com"))
                                  .Returns(false);

            var result = controller.SignUp(new SignUpRequest()
            {
                Email = "bigegg@bigegg.com",
                Password = "Password!@#",
                NickName = "BigEgg",
                RegisterSource = "Website"
            });
            Assert.IsType(typeof(CreatedAtRouteResult), result);
            Assert.Equal(201, (result as CreatedAtRouteResult).StatusCode);
            Assert.Equal("LogIn", (result as CreatedAtRouteResult).RouteName);

            applicationUserService.Reset();
        }
    }
}
