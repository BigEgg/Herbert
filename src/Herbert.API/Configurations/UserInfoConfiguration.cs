namespace Herbert.API.Configurations
{
    using Microsoft.Extensions.DependencyInjection;

    using Herbert.DAL.Repositories;
    using Herbert.DAL.Repositories.Interfaces;
    using Herbert.Services.UserInfo;

    /// <summary>
    /// The extension class for setup dependency injection
    /// </summary>
    public static class UserInfoConfiguration
    {
        /// <summary>
        /// Setups the dependency injection related with user information.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void SetupUserInfo(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IPasswordEncryptHandler, PasswordEncryptHandler>();
        }
    }
}
