namespace Herbert.API.Configurations
{
    using Microsoft.Extensions.DependencyInjection;

    using Herbert.DAL.Repositories;
    using Herbert.DAL.Repositories.Interfaces;
    using Herbert.Services.Access;

    /// <summary>
    /// The extension class for setup dependency injection
    /// </summary>
    public static class AccessConfiguration
    {
        /// <summary>
        /// Setups the dependency injection related with access logic.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void SetupAccess(this IServiceCollection services)
        {
            services.AddScoped<ISupportApplicationRepository, SupportApplicationRepository>();
            services.AddTransient<ISupportApplicationService, SupportApplicationService>();
        }
    }
}
