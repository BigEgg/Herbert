namespace Herbert.Services.Access
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Caching.Memory;

    using Herbert.DAL.Repositories.Interfaces;
    using Herbert.Models.Access;

    /// <summary>
    /// The business logicals for <see cref="SupportApplication"/> entity.
    /// </summary>
    /// <seealso cref="Herbert.Services.Access.ISupportApplicationService" />
    public class SupportApplicationService : ISupportApplicationService
    {
        private const string SUPPORT_APPLICATION_CACHE_KEY = "SUPPORT_APPLICATION_CACHE_KEY";
        private readonly ISupportApplicationRepository repository;
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportApplicationService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="memoryCache">The memory cache.</param>
        public SupportApplicationService(
            ISupportApplicationRepository repository,
            IMemoryCache memoryCache)
        {
            this.repository = repository;
            this.memoryCache = memoryCache;
        }


        /// <summary>
        /// Gets the support application type.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="appSecret">The application secret.</param>
        /// <returns>
        /// The support application type if matches, otherwise null.
        /// </returns>
        /// <exception cref="System.ArgumentException">App Secret cannot bet empty.</exception>
        public SupportApplicationType? GetApplicationType(Guid appId, string appSecret)
        {
            if (string.IsNullOrWhiteSpace(appSecret)) { throw new ArgumentException("App Secret cannot bet empty."); }

            var supportApplications = memoryCache.Get<IList<SupportApplication>>(SUPPORT_APPLICATION_CACHE_KEY);
            if (supportApplications == null)
            {
                supportApplications = repository.GetAll();
                memoryCache.Set(SUPPORT_APPLICATION_CACHE_KEY, supportApplications,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
            }

            var application = supportApplications.FirstOrDefault(a => a.AppId.Equals(appId) && string.Equals(a.AppSecret, appSecret));
            return application?.ApplicationType;
        }
    }
}
