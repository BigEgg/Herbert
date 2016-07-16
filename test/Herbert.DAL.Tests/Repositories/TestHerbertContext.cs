namespace Herbert.DAL.Tests.Repositories
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;

    public class TestHerbertContext : HerbertContext, IDisposable
    {
        private readonly static DbContextOptions<HerbertContext> options;
        private readonly IDbContextTransaction transaction;

        static TestHerbertContext()
        {
            options = CreateNewContextOptions(GetConfiguration());
        }

        public TestHerbertContext() : base(options)
        {
            transaction = base.Database.BeginTransaction();
        }

        public override void Dispose()
        {
            transaction.Rollback();
            transaction.Dispose();
            base.Dispose();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            return builder.Build();
        }

        private static DbContextOptions<HerbertContext> CreateNewContextOptions(IConfigurationRoot configuration)
        {
            var builder = new DbContextOptionsBuilder<HerbertContext>();
            builder.UseSqlServer(configuration["Data:HerbertConnection:ConnectionString"]);

            return builder.Options;
        }
    }
}
