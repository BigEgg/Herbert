namespace Herbert.Data.Tests.Repositories
{
    using System;
    using Xunit;

    using Herbert.Data.Repositories;
    using Herbert.Model.UserInfo;

    public class ApplicationUserRepositoryTests : RepositoryTestBase
    {
        [Fact(DisplayName = "Can add multiple user")]
        public void TestAddNewUser()
        {
            using (var context = new HerbertContext(CreateNewContextOptions()))
            {
                var repository = new ApplicationUserRepository(context);

                repository.AddNewUser("email1@email.com", "encryptPassword", "BigEgg", RegisterSourceType.Website);

                repository.AddNewUser("email2@email.com", "encryptPassword", "BigEgg", RegisterSourceType.Website);
            }
        }

        [Fact(DisplayName = "Should not add a user when Email already exist")]
        public void TestAddNewUser_DuplicateEmail()
        {
            using (var context = new HerbertContext(CreateNewContextOptions()))
            {
                var repository = new ApplicationUserRepository(context);

                repository.AddNewUser("email@email.com", "encryptPassword", "BigEgg", RegisterSourceType.Website);

                Assert.Throws<InvalidOperationException>(() => 
                    repository.AddNewUser("email@email.com", "encryptPassword", "BigEgg", RegisterSourceType.Website)
                );
            }
        }

        [Fact(DisplayName = "Can store the user info into DB correctly")]
        public void TestGetUser()
        {
            using (var context = new HerbertContext(CreateNewContextOptions()))
            {
                var repository = new ApplicationUserRepository(context);

                repository.AddNewUser("email@email.com", "encryptPassword", "BigEgg", RegisterSourceType.Website);

                var user = repository.GetUser("email@email.com");

                Assert.NotNull(user.Id);
                Assert.Equal("email@email.com", user.Email, false);
                Assert.Equal("encryptPassword", user.Password, false);
                Assert.Equal("BigEgg", user.NickName, false);
                Assert.Equal(RegisterSourceType.Website, user.RegisterSource);
                Assert.Equal(UserRole.User, user.Role);

                Assert.True(user.CreatedTime > DateTime.UtcNow.AddMinutes(-10), "Created Time should be set.");
                Assert.True(user.LastUpdated > DateTime.UtcNow.AddMinutes(-10), "Last Updated Time should be set.");
            }
        }
    }
}
