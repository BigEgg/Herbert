namespace Herbert.Models.Access
{
    using System;

    /// <summary>
    /// The access token for user log in
    /// </summary>
    public class UserAccessToken
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the access token expired time.
        /// </summary>
        /// <value>
        /// The access token expired time.
        /// </value>
        public DateTime AccessTokenExpiredTime { get; set; }

        /// <summary>
        /// Gets or sets the access token for application type.
        /// </summary>
        /// <value>
        /// The application type.
        /// </value>
        public SupportApplicationType ApplicationType { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token expired time.
        /// </summary>
        /// <value>
        /// The refresh token expired time.
        /// </value>
        public DateTime RefreshTokenExpiredTime { get; set; }

        /// <summary>
        /// Gets or set the GUID of this user
        /// </summary>
        /// <value>
        /// The GUID of this user.
        /// </value>
        public Guid UserId { get; set; }
    }
}
