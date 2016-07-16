namespace Herbert.DAL.Repositories
{
    using System;
    using System.Linq;

    using Herbert.DAL.Repositories.Interfaces;
    using Herbert.Models.UserInfo;

    /// <summary>
    /// The DAL logicals for <see cref="ApplicationUser"/> entity.
    /// </summary>
    /// <seealso cref="Herbert.DAL.Repositories.RepositoryBase" />
    /// <seealso cref="Herbert.DAL.Repositories.Interfaces.IApplicationUserRepository" />
    public class ApplicationUserRepository : RepositoryBase, IApplicationUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserRepository"/> class.
        /// </summary>
        /// <param name="context">The Herbert DB Context.</param>
        public ApplicationUserRepository(HerbertContext context) : base(context)
        {
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <param name="nickName">The nick name.</param>
        /// <param name="registerSource">The register source.</param>
        public void AddNewUser(string email, string encryptedPassword, string nickName, RegisterSourceType registerSource)
        {
            Context.ApplicationUsers
                .Add(new ApplicationUser()
                {
                    Email = email,
                    Password = encryptedPassword,
                    NickName = nickName,
                    RegisterSource = registerSource,
                    Role = UserRole.User,
                    CreatedTime = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                });
            Context.SaveChanges();
        }

        /// <summary>
        /// Gets The application user with specific email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// The application user.
        /// </returns>
        public ApplicationUser GetUser(string email)
        {
            return Context.ApplicationUsers
                .FirstOrDefault(u => u.Email.Equals(
                    email, 
                    StringComparison.OrdinalIgnoreCase
                ));
        }
    }
}