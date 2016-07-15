namespace Herbert.Model.UserInfo
{
    using System;

    /// <summary>
    /// The Application User
    /// </summary>
    public class ApplicationUser
    {
        /// <summary>
        /// Get and set the GUID of this user
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Get and set the email address of this user, this will need when sign in
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Get and set the password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Get and set the nick name of user
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// Get and set the nick name of user
        /// </summary>
        public UserRole Role { get; set; }
        /// <summary>
        /// Get and set the register source type of user
        /// </summary>
        public RegisterSourceType RegisterSource  { get; set; }

        /// <summary>
        /// Get and set the <see cref="DateTime"/> when this user created.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Get and set the latest <see cref="DateTime"/> when this user update information.
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }
}
