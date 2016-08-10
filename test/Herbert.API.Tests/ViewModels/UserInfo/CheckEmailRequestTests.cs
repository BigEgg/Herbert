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
        [InlineData("bigegg@bigegg.com")]
        [InlineData("YmlnZWdnLmNvbQ==")]
        [InlineData("YmlnZWdnLmNvbQ==abc")]
        [InlineData("YmlnZWdnLmNvb===")]
        [InlineData("YmlnZWdnLmNvbQ!@#==")]
        [InlineData("YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXoxMjM0NTY3ODkwQUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVphYmNkZWZnaGlqa2xtbm9wcXJzdHV2d3h5ejEyMzQ1Njc4OTBBQkNERUZHSElKS0xNTk9QUVJTVFVWV1hZWmFiY2RlZmdoaWprbG1ub3BxcnN0dXZ3eHl6MTIzNDU2Nzg5MEFCQ0RFRkdISUpLTE1OT1BRUlNUVVZXWFlaYWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXoxMjM0NTY3ODkwQUJDREVGR0hJSktMTU5PUFFSU1RVVldYQGJpZ2VnZy5jb20=")]
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
        public void TestCheckEmailRequestValidation_LongButNotOverflow()
        {
            var model = new CheckEmailRequest()
            {
                Email = "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXoxMjM0NTY3ODkwQUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVphYmNkZWZnaGlqa2xtbm9wcXJzdHV2d3h5ejEyMzQ1Njc4OTBBQkNERUZHSElKS0xNTk9QUVJTVFVWV1hZWmFiY2RlZmdoaWprbG1ub3BxcnN0dXZ3eHl6MTIzNDU2Nzg5MEFCQ0RFRkdISUpLTE1OT1BRUlNUVVZXWFlaYWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXoxMjM0NTY3ODkwQUJDREVGR0hJSktMTU5PUFFSU1RVVldAYmlnZWdnLmNvbQ=="
            };

            var result = model.Validate(null);
            Assert.False(result.Any());
        }

        [Fact(DisplayName = "Long Email should valid")]
        public void TestCheckEmailRequestValidation_LongButNotOverflow_WithoutPadding()
        {
            var model = new CheckEmailRequest()
            {
                Email = "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXoxMjM0NTY3ODkwQUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVphYmNkZWZnaGlqa2xtbm9wcXJzdHV2d3h5ejEyMzQ1Njc4OTBBQkNERUZHSElKS0xNTk9QUVJTVFVWV1hZWmFiY2RlZmdoaWprbG1ub3BxcnN0dXZ3eHl6MTIzNDU2Nzg5MEFCQ0RFRkdISUpLTE1OT1BRUlNUVVZXWFlaYWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXoxMjM0NTY3ODkwQUJDREVGR0hJSktMTU5PUFFSU1RVVldAYmlnZWdnLmNvbQ"
            };

            var result = model.Validate(null);
            Assert.False(result.Any());
        }

        [Fact(DisplayName = "Normal Email should valid")]
        public void TestCheckEmailRequestValidation_Normal()
        {
            var model = new CheckEmailRequest()
            {
                Email = "YmlnZWdnQGJpZ2VnZy5jb20="
            };

            var result = model.Validate(null);
            Assert.False(result.Any());
        }

        [Fact(DisplayName = "Normal Email should valid")]
        public void TestCheckEmailRequestValidation_Normal_WithoutPadding()
        {
            var model = new CheckEmailRequest()
            {
                Email = "YmlnZWdnQGJpZ2VnZy5jb20"
            };

            var result = model.Validate(null);
            Assert.False(result.Any());
        }
    }
}
