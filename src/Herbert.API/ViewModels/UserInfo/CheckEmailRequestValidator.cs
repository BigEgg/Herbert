namespace Herbert.API.ViewModels.UserInfo
{
    using FluentValidation;

    /// <summary>
    /// The validator class for <see cref="CheckEmailRequest"/> class.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{EmailCheckRequest}" />
    internal class CheckEmailRequestValidator : AbstractValidator<CheckEmailRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckEmailRequestValidator"/> class.
        /// </summary>
        public CheckEmailRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(1, 256);
        }
    }
}