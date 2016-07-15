namespace Herbert.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Herbert.Model.UserInfo;

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
                .HasMaxLength(255)
                .IsConcurrencyToken();

            entityBuilder.Property(u => u.Password)
                .IsRequired();

            entityBuilder.Property(u => u.NickName)
                .IsRequired();

            entityBuilder.Property(u => u.CreatedTime)
                .IsConcurrencyToken()
                .IsRequired();

            entityBuilder.Property(u => u.LastUpdated)
                .IsRequired();
        }
    }
}
