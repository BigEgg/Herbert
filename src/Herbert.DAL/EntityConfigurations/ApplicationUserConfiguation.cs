namespace Herbert.DAL.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Herbert.Models.UserInfo;

    /// <summary>
    /// The DB Context configuration for Application User model
    /// </summary>
    public static class ApplicationUserConfiguation
    {
        /// <summary>
        /// Setups the contracts for <see cref="ApplicationUser"/> entity.
        /// </summary>
        /// <param name="entityBuilder">The entity builder.</param>
        public static void SetupContracts(this EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasKey(u => u.Id);

            entityBuilder.HasAlternateKey(u => u.Email);

            entityBuilder.HasIndex(u => u.Email);

            entityBuilder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256)
                .IsConcurrencyToken();

            entityBuilder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(256);

            entityBuilder.Property(u => u.NickName)
                .IsRequired()
                .HasMaxLength(64);

            entityBuilder.Property(u => u.CreatedTime)
                .IsConcurrencyToken()
                .IsRequired();

            entityBuilder.Property(u => u.LastUpdated)
                .IsRequired();
        }
    }
}
