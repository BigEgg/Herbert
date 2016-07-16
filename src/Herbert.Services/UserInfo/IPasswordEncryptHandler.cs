namespace Herbert.Services.UserInfo
{
    /// <summary>
    /// Password Encrypt Handler
    /// </summary>
    public interface IPasswordEncryptHandler
    {
        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The encrypted password.</returns>
        string EncryptPassword(string password);

        /// <summary>
        /// Compares the password with encrypted password to see if they are matched.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <returns><c>True</c> if password and encrypted password are matched, otherwise <c>false</c>.</returns>
        bool ComparePassword(string password, string encryptedPassword);
    }
}
