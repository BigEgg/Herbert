namespace Herbert.Services.UserInfo
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Password Encrypt Handler
    /// </summary>
    /// <seealso cref="Herbert.Services.UserInfo.IPasswordEncryptHandler" />
    public class PasswordEncryptHandler : IPasswordEncryptHandler
    {
        private const int SALT_LENGTH = 16;
        private const int PASSWORD_LENGTH = 256;

        /// <summary>
        /// Compares the password with encrypted password to see if they are matched.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <returns>
        ///   <c>True</c> if password and encrypted password are matched, otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Password cannot be empty.
        /// or
        /// Encrypted Password cannot be empty.
        /// </exception>
        public bool ComparePassword(string password, string encryptedPassword)
        {
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentException("Password cannot be empty."); }
            if (string.IsNullOrWhiteSpace(encryptedPassword)) { throw new ArgumentException("Encrypted Password cannot be empty."); }

            var salt = encryptedPassword.Substring(PASSWORD_LENGTH - SALT_LENGTH);
            var newEncryptedPassword = EncryptPassword(password, salt);

            return string.Compare(newEncryptedPassword, encryptedPassword) == 0;
        }

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>
        /// The encrypted password.
        /// </returns>
        /// <exception cref="System.ArgumentException">Password cannot be empty.</exception>
        public string EncryptPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentException("Password cannot be empty."); }

            return EncryptPassword(password, GenerateSalt());
        }

        private string EncryptPassword(string password, string salt)
        {
            var hashAlgorithm = SHA512.Create();

            var result = password;
            byte[] bytes, hashValue;

            int count = 0;
            do
            {
                count++;
                bytes = Encoding.UTF8.GetBytes(result);

                hashValue = hashAlgorithm.ComputeHash(bytes);
                result = Convert.ToBase64String(bytes);
                if (result.Length > PASSWORD_LENGTH - SALT_LENGTH) { result = result.Substring(0, PASSWORD_LENGTH - SALT_LENGTH); }

                result = result + salt;
            } while (result.Length < PASSWORD_LENGTH);

            return result;
        }

        private string GenerateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var buff = new byte[SALT_LENGTH];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff).Substring(0, SALT_LENGTH);
        }
    }
}
