namespace Herbert.API.Helpers
{
    using System;
    using System.Text;

    /// <summary>
    /// The string Base64 Encode/Decode extensions
    /// </summary>
    public static class Base64Helper
    {
        /// <summary>
        /// Decode Base64 format string to raw message.
        /// </summary>
        /// <param name="base64String">The base64 format string.</param>
        /// <returns>The raw message.</returns>
        public static string Base64Decode(this string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String)) { return string.Empty; }

            try
            {
                base64String = base64String.PadRight(base64String.Length + (4 - base64String.Length % 4) % 4, '=');
                byte[] bytes = Convert.FromBase64String(base64String);
                return Encoding.ASCII.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Encode message to Base64 format string.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The Base 64 format string.</returns>
        public static string Base64Encode(this string message)
        {
            if (string.IsNullOrWhiteSpace(message)) { return string.Empty; }

            byte[] bytes = Encoding.ASCII.GetBytes(message);
            return Convert.ToBase64String(bytes);
        }
    }
}
