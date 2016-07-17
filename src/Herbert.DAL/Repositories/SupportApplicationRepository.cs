namespace Herbert.DAL.Repositories
{
    using System;
    using System.Collections.Generic;

    using Herbert.DAL.Repositories.Interfaces;
    using Herbert.Models.Access;
    using System.Linq;

    /// <summary>
    /// The DAL logicals for <see cref="SupportApplication"/> entity.
    /// </summary>
    /// <seealso cref="Herbert.DAL.Repositories.RepositoryBase" />
    /// <seealso cref="Herbert.DAL.Repositories.Interfaces.ISupportApplicationRepository" />
    public class SupportApplicationRepository : RepositoryBase, ISupportApplicationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupportApplicationRepository"/> class.
        /// </summary>
        /// <param name="context">The Herbert DB Context.</param>
        public SupportApplicationRepository(HerbertContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all support applications.
        /// </summary>
        /// <returns>A list contains all support applications</returns>
        public IList<SupportApplication> GetAll()
        {
            return Context.SupportApplications.ToList();
        }
    }
}
