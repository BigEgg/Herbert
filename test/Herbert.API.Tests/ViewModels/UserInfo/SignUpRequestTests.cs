namespace Herbert.API.Tests.ViewModels.UserInfo
{
    using System.Linq;
    using Xunit;

    using Herbert.API.ViewModels.UserInfo;
    using Herbert.Models.UserInfo;

    public class SignUpRequestTests
    {
        [Theory(DisplayName = "Check User Info VM Validation - Email")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("bigegg.com")]
        [InlineData("abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWX@bigegg.com")]
        public void TestRegisterRequestValidation_Email(string email)
        {
            var model = new SignUpRequest()
            {
                Email = email,
                Password = "Password!@#",
                NickName = "BigEgg",
                RegisterSource = "Website"
            };

            var result = model.Validate(null);
            Assert.True(result.Any());
        }

        [Theory(DisplayName = "Check User Info VM Validation - Password")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("abcdefg")]
        [InlineData("abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabc")]
        public void TestRegisterRequestValidation_Password(string password)
        {
            var model = new SignUpRequest()
            {
                Email = "bigegg@bigegg.com",
                Password = password,
                NickName = "BigEgg",
                RegisterSource = "Website"
            };

            var result = model.Validate(null);
            Assert.True(result.Any());
        }

        [Theory(DisplayName = "Check User Info VM Validation - NickName")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabc")]
        public void TestRegisterRequestValidation_NickName(string nickName)
        {
            var model = new SignUpRequest()
            {
                Email = "bigegg@bigegg.com",
                Password = "Password!@#",
                NickName = nickName,
                RegisterSource = "Website"
            };

            var result = model.Validate(null);
            Assert.True(result.Any());
        }

        [Theory(DisplayName = "Check User Info VM Validation - RegisterSource")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("abcdefg")]
        public void TestRegisterRequestValidation_RegisterSource(string registerSource)
        {
            var model = new SignUpRequest()
            {
                Email = "bigegg@bigegg.com",
                Password = "Password!@#",
                NickName = "BigEgg",
                RegisterSource = registerSource
            };

            var result = model.Validate(null);
            Assert.True(result.Any());
        }

        [Fact(DisplayName = "Should valid")]
        public void TestRegisterRequestValidation_Normal()
        {
            var model = new SignUpRequest()
            {
                Email = "bigegg@bigegg.com",
                Password = "Password!@#",
                NickName = "BigEgg",
                RegisterSource = "Website"
            };

            var result = model.Validate(null);
            Assert.False(result.Any());
            Assert.Equal(RegisterSourceType.Website, model.RegisterSourceType);
        }
    }
}
