namespace Herbert.API.ViewModels.UserInfo
{
    using System;
    using FluentValidation;

    using Herbert.Models.UserInfo;

    /// <summary>
    /// The validator class for <see cref="RegisterRequest"/> class.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{RegisterRequest}" />
    internal class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRequestValidator"/> class.
        /// </summary>
        public RegisterRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(viewModel => viewModel.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(1, 256);

            RuleFor(viewModel => viewModel.Password)
                .NotEmpty()
                .Length(1, 256);

            RuleFor(viewModel => viewModel.RepeatPassword)
                .NotEmpty()
                .Length(1, 256)
                .Equal(viewModel => viewModel.Password);

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
