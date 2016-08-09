namespace Herbert.API.Tests.Helpers
{
    using System;
    using Xunit;

    using Herbert.API.Helpers;
    using Herbert.API.ViewModels.Access;

    public class JsonSerializeHelperTests
    {
        [Fact]
        public void TestSerialize_EmptyInput()
        {
            ApplicationTokenVM vm = null;

            Assert.Equal("{}", vm.Serialize());
        }

        [Fact]
        public void TestSerialize()
        {
            var vm = new ApplicationTokenVM()
            {
                AppId = new Guid("1b4f7394-b57e-49c7-b596-80988eaf7362"),
                AppSecret = "KbFHpy7SjvUPa9LdP9MLIBtC5BW3WnDj9YYGI0Fk35n4r35OmQJY1VWGajONG9Y6"
            };
            string expected =
                @"{
                    ""appId"": ""1b4f7394-b57e-49c7-b596-80988eaf7362"",
                    ""appSecret"": ""KbFHpy7SjvUPa9LdP9MLIBtC5BW3WnDj9YYGI0Fk35n4r35OmQJY1VWGajONG9Y6""
                }";

            Assert.Equal(FormatJson(expected), vm.Serialize());
        }

        [Fact]
        public void TestDeserialize_EmptyInput()
        {
            Assert.Null(string.Empty.Deserialize<ApplicationTokenVM>());
            Assert.Null(((string)null).Deserialize<ApplicationTokenVM>());
            Assert.Null("   ".Deserialize<ApplicationTokenVM>());
        }

        [Fact]
        public void TestDeserialize()
        {
            string json =
                @"{
                    ""appId"": ""1b4f7394-b57e-49c7-b596-80988eaf7362"",
                    ""appSecret"": ""KbFHpy7SjvUPa9LdP9MLIBtC5BW3WnDj9YYGI0Fk35n4r35OmQJY1VWGajONG9Y6""
                }";

            var vm = json.Deserialize<ApplicationTokenVM>();

            Assert.NotNull(vm);
            Assert.Equal(new Guid("1b4f7394-b57e-49c7-b596-80988eaf7362"), vm.AppId);
            Assert.Equal("KbFHpy7SjvUPa9LdP9MLIBtC5BW3WnDj9YYGI0Fk35n4r35OmQJY1VWGajONG9Y6", vm.AppSecret);
        }

        [Fact]
        public void TestDeserialize_InvalidInput_NotMatch()
        {
            string json =
                @"{
                    ""id"": ""1b4f7394-b57e-49c7-b596-80988eaf7362"",
                    ""secret"": ""KbFHpy7SjvUPa9LdP9MLIBtC5BW3WnDj9YYGI0Fk35n4r35OmQJY1VWGajONG9Y6""
                }";

            var vm = json.Deserialize<ApplicationTokenVM>();

            Assert.Null(vm);
        }

        [Fact]
        public void TestDeserialize_InvalidInput_InvalidProperty()
        {
            string json =
                @"{
                    ""appId"": ""b57e-49c7-b596-80988eaf7362"",
                    ""appSecret"": ""KbFHpy7SjvUPa9LdP9MLIBtC5BW3WnDj9YYGI0Fk35n4r35OmQJY1VWGajONG9Y6""
                }";

            var vm = json.Deserialize<ApplicationTokenVM>();

            Assert.Null(vm);
        }


        private string FormatJson(string jsonString)
        {
            return jsonString.Replace("\r", "").Replace("\n", "").Replace(" ", "");
        }
    }
}
