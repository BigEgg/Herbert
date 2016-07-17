namespace Herbert.DAL.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Herbert.Models.Access;

    /// <summary>
    /// The DB Context configuration for Support Application model
    /// </summary>
    public static class SupportApplicationConfiguration
    {
        /// <summary>
        /// Setups the contracts for <see cref="SupportApplication"/> entity.
        /// </summary>
        /// <param name="entityBuilder">The entity builder.</param>
        public static void SetupContracts(this EntityTypeBuilder<SupportApplication> entityBuilder)
        {
            entityBuilder.HasKey(a => a.ApplicationType);

            entityBuilder.HasIndex(a => new { a.AppId, a.AppSecret });

            entityBuilder.Property(a => a.AppId)
                .IsRequired()
                .IsConcurrencyToken()
                .ValueGeneratedOnAdd();

            entityBuilder.Property(a => a.AppSecret)
                .IsRequired()
                .HasMaxLength(64);

            entityBuilder.Property(a => a.ApplicationType)
                .IsConcurrencyToken();

            entityBuilder.Property(a => a.CreatedTime)
                .IsConcurrencyToken()
                .IsRequired();

            entityBuilder.Property(a => a.LastUpdated)
                .IsRequired();
        }
    }
}
