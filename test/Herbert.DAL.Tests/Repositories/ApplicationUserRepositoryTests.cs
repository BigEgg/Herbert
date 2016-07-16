namespace Herbert.DAL.Tests.Repositories
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    using Herbert.DAL.Repositories;
    using Herbert.Models.UserInfo;

    public class ApplicationUserRepositoryTests
    {
        [Fact(DisplayName = "Should check contract")]
        public void TestAddNewUser_Contract_Email()
        {
            using (var context = new TestHerbertContext())
            {
                var repository = new ApplicationUserRepository(context);

                Assert.Throws<DbUpdateException>(() =>
                    repository.AddNewUser(new string('A', 257), "encryptPassword", "BigEgg", RegisterSourceType.Website)
                );
            }
        }

        [Fact(DisplayName = "Should check contract")]
        public void TestAddNewUser_Contract_Password()
        {
            using (var context = new TestHerbertContext())
            {
                var repository = new ApplicationUserRepository(context);

                Assert.Throws<DbUpdateException>(() =>
                    repository.AddNewUser("email1@email.com", new string('A', 257), "BigEgg", RegisterSourceType.Website)
                );
            }
        }

        [Fact(DisplayName = "Should check contract")]
        public void TestAddNewUser_Contract_NickName()
        {
            using (var context = new TestHerbertContext())
            {
                var repository = new ApplicationUserRepository(context);

                Assert.Throws<DbUpdateException>(() =>
                    repository.AddNewUser("email1@email.com", "encryptedPassword", new string('A', 65), RegisterSourceType.Website)
                );
            }
        }

        [Fact(DisplayName = "Can add multiple user")]
        public void TestAddNewUser()
        {
            using (var context = new TestHerbertContext())
            {
                var repository = new ApplicationUserRepository(context);

                repository.AddNewUser("email1@email.com", "encryptedPassword", "BigEgg", RegisterSourceType.Website);

                repository.AddNewUser("email2@email.com", "encryptedPassword", "BigEgg", RegisterSourceType.Website);
            }
        }

        [Fact(DisplayName = "Should not add a user when Email already exist")]
        public void TestAddNewUser_DuplicateEmail()
        {
            using (var context = new TestHerbertContext())
            {
                var repository = new ApplicationUserRepository(context);

                repository.AddNewUser("email@email.com", "encryptedPassword", "BigEgg", RegisterSourceType.Website);

                Assert.Throws<InvalidOperationException>(() => 
                    repository.AddNewUser("email@email.com", "encryptedPassword", "BigEgg", RegisterSourceType.Website)
                );
            }
        }

        [Fact(DisplayName = "Can store the user info into DB correctly")]
        public void TestGetUser()
        {
            using (var context = new TestHerbertContext())
            {
                var repository = new ApplicationUserRepository(context);

                repository.AddNewUser("email@email.com", "encryptedPassword", "BigEgg", RegisterSourceType.Website);

                var user = repository.GetUser("email@email.com");

                Assert.NotNull(user.Id);
                Assert.Equal("email@email.com", user.Email, false);
                Assert.Equal("encryptedPassword", user.Password, false);
                Assert.Equal("BigEgg", user.NickName, false);
                Assert.Equal(RegisterSourceType.Website, user.RegisterSource);
                Assert.Equal(UserRole.User, user.Role);

                Assert.True(user.CreatedTime > DateTime.UtcNow.AddMinutes(-10), "Created Time should be set.");
                Assert.True(user.LastUpdated > DateTime.UtcNow.AddMinutes(-10), "Last Updated Time should be set.");
            }
        }

        [Fact(DisplayName = "Get null when no matched user info in DB")]
        public void TestGetUser_NotExist()
        {
            using (var context = new TestHerbertContext())
            {
                var repository = new ApplicationUserRepository(context);

                Assert.Null(repository.GetUser("email@email.com"));
            }
        }
    }
}
