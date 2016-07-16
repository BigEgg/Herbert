namespace Herbert.Services.UserInfo
{
    using Herbert.Models.UserInfo;

    /// <summary>
    /// The business logicals for <see cref="ApplicationUser"/> entity.
    /// </summary>
    public interface IApplicationUserService
    {
        /// <summary>
        /// Determines whether the email had already been used.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns><c>True</c> if this email had already been used, otherwise <c>false</c>.</returns>
        bool IsEmailAlreadyUsed(string email);

        /// <summary>
        /// Gets the user with the specific email and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        ApplicationUser GetUser(string email, string password);

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="nickName">The nick name.</param>
        /// <param name="registerSource">The register source.</param>
        ApplicationUser NewUser(string email, string password, string nickName, RegisterSourceType registerSource);
    }
}
