namespace Herbert.Data.Repositories.Interfaces
{
    using Herbert.Model.UserInfo;

    /// <summary>
    /// Repository for Application User entity
    /// </summary>
    public interface IApplicationUserRepository
    {
        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="encryptPassword">The encrypt password.</param>
        /// <param name="nickName">The nick name.</param>
        /// <param name="registerSource">The register source.</param>
        void NewUser(string email, string encryptPassword, string nickName, RegisterSourceType registerSource);

        /// <summary>
        /// Gets The application user with specific email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The application user.</returns>
        ApplicationUser GetUser(string email);
    }
}