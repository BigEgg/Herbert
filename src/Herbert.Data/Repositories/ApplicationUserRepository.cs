namespace Herbert.Data.Repositories
{
    using System;
    using System.Linq;

    using Herbert.Data.Repositories.Interfaces;
    using Herbert.Model.UserInfo;

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
        /// <param name="encryptPassword">The encrypt password.</param>
        /// <param name="nickName">The nick name.</param>
        /// <param name="registerSource">The register source.</param>
        public void AddNewUser(string email, string encryptPassword, string nickName, RegisterSourceType registerSource)
        {
            Context.ApplicationUsers
                .Add(new ApplicationUser()
                {
                    Email = email,
                    Password = encryptPassword,
                    NickName = nickName,
                    RegisterSource = registerSource,
                    Role = UserRole.User
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