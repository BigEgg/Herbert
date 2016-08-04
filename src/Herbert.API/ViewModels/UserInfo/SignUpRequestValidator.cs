namespace Herbert.API.ViewModels.UserInfo
{
    using System;
    using FluentValidation;

    using Herbert.Models.UserInfo;

    /// <summary>
    /// The validator class for <see cref="SignUpRequest"/> class.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{RegisterRequest}" />
    internal class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpRequestValidator"/> class.
        /// </summary>
        public SignUpRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(1, 256);

            RuleFor(viewModel => viewModel.Password)
                .NotEmpty()
                .Length(8, 64);

            RuleFor(viewModel => viewModel.NickName)
                .NotEmpty()
                .Length(1, 64);

            RuleFor(viewModel => viewModel.RegisterSource)
                .NotEmpty()
                .Must(registerSource =>
                {
                    RegisterSourceType type;
                    return Enum.TryParse(registerSource, out type);
                });
        }
    }
}
