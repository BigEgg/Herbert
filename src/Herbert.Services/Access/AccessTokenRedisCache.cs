namespace Herbert.Services.Access
{
    using System;

    using Herbert.Models.Access;

    /// <summary>
    /// The Redis Cache repository for <see cref="UserAccessToken"/> model
    /// </summary>
    /// <seealso cref="IAccessTokenCache" />
    public class AccessTokenRedisCache : IAccessTokenCache
    {
        /// <summary>
        /// Create a new access token.
        /// </summary>
        /// <param name="userAccessToken">The user access token.</param>
        public void AddAccessToken(UserAccessToken userAccessToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user access token by specific access token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>
        /// The user access token
        /// </returns>
        public UserAccessToken GetAccessToken(string accessToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the access token.
        /// </summary>
        /// <param name="oldAccessToken">The old access token.</param>
        /// <param name="newAccessToken">The new access token.</param>
        public void UpdateAccessToken(string oldAccessToken, string newAccessToken)
        {
            throw new NotImplementedException();
        }
    }
}
