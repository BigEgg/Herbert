namespace Herbert.API.ViewModels.UserInfo
{
    /// <summary>
    /// The response view model for check is email exist
    /// </summary>
    public class CheckEmailResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckEmailResponse"/> class.
        /// </summary>
        /// <param name="isUsed">if set to <c>true</c> [is used].</param>
        public CheckEmailResponse(bool isUsed)
        {
            IsUsed = isUsed;
        }

        /// <summary>
        /// Gets a value indicating whether this email had been used.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this email had been used; otherwise, <c>false</c>.
        /// </value>
        public bool IsUsed { get; private set; }
    }
}
