namespace Herbert.Services.UserInfo
{
    using System;

    using Herbert.DAL.Repositories.Interfaces;
    using Herbert.Models.UserInfo;

    /// <summary>
    /// The business logicals for <see cref="ApplicationUser"/> entity.
    /// </summary>
    /// <seealso cref="Herbert.Services.UserInfo.IApplicationUserService" />
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository repository;
        private readonly IPasswordEncryptHandler passwordEncryptHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserService"/> class.
        /// </summary>
        /// <param name="repository">The repository of Application User entity.</param>
        /// <param name="passwordEncryptHandler">The password encrypt handler.</param>
        public ApplicationUserService(IApplicationUserRepository repository, IPasswordEncryptHandler passwordEncryptHandler)
        {
            this.repository = repository;
            this.passwordEncryptHandler = passwordEncryptHandler;
        }


        /// <summary>
        /// Gets the user with the specific email and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Email cannot be empty.
        /// or
        /// password cannot be empty.
        /// </exception>
        public ApplicationUser GetUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentException("Email cannot be empty."); }
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentException("Password cannot be empty."); }

            var user = repository.GetUser(email);
            if (user == null) { return null; }

            return passwordEncryptHandler.ComparePassword(password, user.Password)
                ? user
                : null;
        }

        /// <summary>
        /// Determines whether the email had already been used.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   <c>True</c> if this email had already been used, otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentException">Email cannot be empty.</exception>
        public bool IsEmailAlreadyUsed(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentException("Email cannot be empty."); }

            return repository.GetUser(email) != null;
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="nickName">The nick name.</param>
        /// <param name="registerSource">The register source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Email cannot be empty.
        /// or
        /// Password cannot be empty.
        /// or
        /// Nick name cannot be empty.
        /// </exception>
        public ApplicationUser NewUser(string email, string password, string nickName, RegisterSourceType registerSource)
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentException("Email cannot be empty."); }
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentException("Password cannot be empty."); }
            if (string.IsNullOrWhiteSpace(nickName)) { throw new ArgumentException("Nick name cannot be empty."); }

            var user = repository.GetUser(email);
            if (user != null) { throw new InvalidOperationException("User with that email already exist."); }

            repository.AddNewUser(
                email,
                passwordEncryptHandler.EncryptPassword(password),
                nickName,
                registerSource);

            return repository.GetUser(email);
        }
    }
}
