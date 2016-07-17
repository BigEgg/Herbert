namespace Herbert.Models.Access
{
    using System;

    /// <summary>
    /// The support application model to identify where API come
    /// </summary>
    public class SupportApplication
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

        /// <summary>
        /// Gets or sets the type of the support application.
        /// </summary>
        /// <value>
        /// The type of the support application.
        /// </value>
        public SupportApplicationType ApplicationType { get; set; }

        /// <summary>
        /// Get or set the <see cref="DateTime"/> when this Support Application model created.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Get or set the latest <see cref="DateTime"/> when this Support Application model update information.
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }
}
