namespace Herbert.API.ViewModels.Access
{
    using System;

    /// <summary>
    /// The Application Token View Model
    /// </summary>
    public class ApplicationTokenVM
    {
        /// <summary>
        /// Gets or sets the support application identifier.
        /// </summary>
        /// <value>
        /// The support application identifier.
        /// </value>
        public Guid AppId { get; set; }

        /// <summary>
        /// Gets or sets the support application secret.
        /// </summary>
        /// <value>
        /// The support application secret.
        /// </value>
        public string AppSecret { get; set; }

    }
}
