namespace Herbert.Services.Access
{
    using System;

    using Herbert.Models.Access;

    /// <summary>
    /// The business logicals for <see cref="SupportApplication"/> entity.
    /// </summary>
    public interface ISupportApplicationService
    {
        /// <summary>
        /// Gets the support application type.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="appSecret">The application secret.</param>
        /// <returns>The support application type if matches, otherwise null.</returns>
        SupportApplicationType? GetApplicationType(Guid appId, string appSecret);
    }
}
