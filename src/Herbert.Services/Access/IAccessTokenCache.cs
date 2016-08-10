namespace Herbert.Services.Access
{
    using Herbert.Models.Access;

    /// <summary>
    /// The Cache repository for <see cref="UserAccessToken"/> model
    /// </summary>
    public interface IAccessTokenCache
    {
        /// <summary>
        /// Create a new access token.
        /// </summary>
        /// <param name="userAccessToken">The user access token.</param>
        void AddAccessToken(UserAccessToken userAccessToken);

        /// <summary>
        /// Gets the user access token by specific access token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>The user access token</returns>
        UserAccessToken GetAccessToken(string accessToken);

        /// <summary>
        /// Updates the access token.
        /// </summary>
        /// <param name="oldAccessToken">The old access token.</param>
        /// <param name="newAccessToken">The new access token.</param>
        void UpdateAccessToken(string oldAccessToken, string newAccessToken);
    }
}
