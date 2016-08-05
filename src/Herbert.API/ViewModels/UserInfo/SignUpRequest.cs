namespace Herbert.API.ViewModels.UserInfo
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Herbert.Models.UserInfo;

    /// <summary>
    /// The request view model for register a new user
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.IValidatableObject" />
    public class SignUpRequest : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the nick name.
        /// </summary>
        /// <value>
        /// The nick name.
        /// </value>
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the register source.
        /// </summary>
        /// <value>
        /// The register source.
        /// </value>
        public string RegisterSource { get; set; }

        /// <summary>
        /// Gets the type of the register source.
        /// </summary>
        /// <value>
        /// The type of the register source.
        /// </value>
        internal RegisterSourceType RegisterSourceType
        {
            get
            {
                return (RegisterSourceType)Enum.Parse(typeof(RegisterSourceType), RegisterSource);
            }
        }


        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new SignUpRequestValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
