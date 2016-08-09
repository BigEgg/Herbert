namespace Herbert.API.Tests.Helpers
{
    using Xunit;

    using Herbert.API.Helpers;

    public class Base64HelperTests
    {
        [Fact]
        public void TestBase64Encode_EmptyInput()
        {
            Assert.Equal(string.Empty, string.Empty.Base64Encode());
            Assert.Equal(string.Empty, ((string)null).Base64Encode());
            Assert.Equal(string.Empty, "    ".Base64Encode());
        }

        [Fact]
        public void TestBase64Encode()
        {
            Assert.Equal("YWJjZGVmZw==", "abcdefg".Base64Encode());
            Assert.Equal("YWJjQGJpZ2VnZy5jb20=", "abc@bigegg.com".Base64Encode());
            Assert.Equal("ZGF0YS5kYXRhLmRhdGE=", "data.data.data".Base64Encode());
        }

        [Fact]
        public void TestBase64Decode_EmptyInput()
        {
            Assert.Equal(string.Empty, string.Empty.Base64Encode());
            Assert.Equal(string.Empty, ((string)null).Base64Encode());
            Assert.Equal(string.Empty, "    ".Base64Encode());
        }

        [Fact]
        public void TestBase64Decode()
        {
            Assert.Equal("abcdefg", "YWJjZGVmZw==".Base64Decode());
            Assert.Equal("abc@bigegg.com", "YWJjQGJpZ2VnZy5jb20=".Base64Decode());
            Assert.Equal("data.data.data", "ZGF0YS5kYXRhLmRhdGE=".Base64Decode());
        }

        [Fact]
        public void TestBase64Decode_WithoutPadding()
        {
            Assert.Equal("abcdefg", "YWJjZGVmZw".Base64Decode());
            Assert.Equal("abc@bigegg.com", "YWJjQGJpZ2VnZy5jb20".Base64Decode());
            Assert.Equal("data.data.data", "ZGF0YS5kYXRhLmRhdGE".Base64Decode());
        }

        [Fact]
        public void TestBase64Decode_InvalidInput()
        {
            Assert.Equal(string.Empty, "YWJjZGVmZwabcdefg==".Base64Decode());
            Assert.Equal(string.Empty, "YWJjZGVmZ===".Base64Decode());
            Assert.Equal(string.Empty, "~!@YWJjZGVmZw".Base64Decode());
        }
    }
}
