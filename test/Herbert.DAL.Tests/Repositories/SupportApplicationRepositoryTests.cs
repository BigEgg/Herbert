namespace Herbert.DAL.Tests.Repositories
{
    using Xunit;

    using Herbert.DAL.Repositories;
    using Herbert.Models.Access;
    using System.Linq;

    public class SupportApplicationRepositoryTests
    {
        [Fact(DisplayName = "Can get all support application as a list")]
        public void TestGetAll()
        {
            using (var context = new TestHerbertContext())
            {
                var respository = new SupportApplicationRepository(context);

                var supportApplications = respository.GetAll();
                Assert.True(supportApplications.Any(a => a.ApplicationType == SupportApplicationType.Website));
            }
        }
    }
}
