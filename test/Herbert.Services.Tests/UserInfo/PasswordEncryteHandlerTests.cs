namespace Herbert.Services.Tests.UserInfo
{
    using System;
    using Xunit;

    using Herbert.Services.UserInfo;

    public class PasswordEncryteHandlerTests
    {
        private readonly PasswordEncryptHandler handler = new PasswordEncryptHandler();

        [Theory(DisplayName = "Should check contract")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void TestEncryptPassword_Contract(string password)
        {
            Assert.Throws<ArgumentException>(() => handler.EncryptPassword(password));
        }

        [Theory(DisplayName = "Should able success generate an 256 length encrypted password")]
        [InlineData("abcdefg")]
        [InlineData("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [InlineData("1qazXDR%@WSXcft6")]
        public void TestEncryptPassword(string password)
        {
            var result = handler.EncryptPassword(password);

            Assert.Equal(256, result.Length);
        }

        [Theory(DisplayName = "Should not generate same encrypted password")]
        [InlineData("abcdefg")]
        [InlineData("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [InlineData("1qazXDR%@WSXcft6")]
        public void TestEncryptPassword_SameResult(string password)
        {
            var result1 = handler.EncryptPassword(password);
            var result2 = handler.EncryptPassword(password);

            Assert.NotEqual(result1, result2);
        }

        [Theory(DisplayName = "Should check contract")]
        [InlineData("", "abcdefg")]
        [InlineData("abcdefg", "")]
        [InlineData(null, "abcdefg")]
        [InlineData("abcdefg", null)]
        [InlineData("    ", "abcdefg")]
        [InlineData("abcdefg", "    ")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("    ", "    ")]
        public void TestComparePassword_Contract(string password, string encryptedPassword)
        {
            Assert.Throws<ArgumentException>(() => handler.ComparePassword(password, encryptedPassword));
        }

        [Theory(DisplayName = "Should able return true when password matchs")]
        [InlineData("abcdefg")]
        [InlineData("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [InlineData("1qazXDR%@WSXcft6")]
        public void TestComparePassword_Match(string password)
        {
            var result = handler.EncryptPassword(password);

            Assert.True(handler.ComparePassword(password, result));
        }

        [Theory(DisplayName = "Should able return false when password not matchs")]
        [InlineData("abcdefg")]
        [InlineData("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [InlineData("1qazXDR%@WSXcft6")]
        public void TestComparePassword_NotMatch(string password)
        {
            var result = handler.EncryptPassword(password);

            Assert.False(handler.ComparePassword(password + "123", result));
        }
    }
}
