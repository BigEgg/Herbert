namespace Herbert.DAL.Repositories.Interfaces
{
    using System.Collections.Generic;

    using Herbert.Models.Access;

    /// <summary>
    /// The DAL logicals for <see cref="SupportApplication"/> entity.
    /// </summary>
    public interface ISupportApplicationRepository
    {
        /// <summary>
        /// Gets all support applications.
        /// </summary>
        /// <returns>A list contains all support applications</returns>
        IList<SupportApplication> GetAll();
    }
}
