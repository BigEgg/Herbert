namespace Herbert.Data.Repositories
{
    /// <summary>
    /// The basic class for Repository
    /// </summary>
    public abstract class RepositoryBase
    {
        private HerbertContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        /// <param name="context">The Herbert DB Context.</param>
        public RepositoryBase(HerbertContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the Herbert DB Context.
        /// </summary>
        /// <value>
        /// The Herbert DB Context.
        /// </value>
        public HerbertContext Context { get { return this.context; } }
    }
}
