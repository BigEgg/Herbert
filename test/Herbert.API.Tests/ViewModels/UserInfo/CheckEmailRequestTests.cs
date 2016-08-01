namespace Herbert.API.Tests.ViewModels.UserInfo
{
    using System.Linq;
    using Xunit;

    using Herbert.API.ViewModels.UserInfo;

    public class CheckEmailRequestTests
    {
        [Theory(DisplayName = "Check User Info VM Validation - Email")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("bigegg.com")]
        [InlineData("abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWX@bigegg.com")]
        public void TestCheckEmailRequestValidation(string email)
        {
            var model = new CheckEmailRequest()
            {
                Email = email
            };

            var result = model.Validate(null);
            Assert.True(result.Any());
        }

        [Fact(DisplayName = "Long Email should valid")]
        public void TestCheckEmailRequestvalidatoin_LongButNotOverflow()
        {
            var model = new CheckEmailRequest()
            {
                Email = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVW@bigegg.com"
            };

            var result = model.Validate(null);
            Assert.False(result.Any());
        }
    }
}
