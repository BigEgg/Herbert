namespace Herbert.Services.Tests.UserInfo
{
    using System;
    using Moq;
    using Xunit;

    using Herbert.DAL.Repositories.Interfaces;
    using Herbert.Models.UserInfo;
    using Herbert.Services.UserInfo;

    public class ApplicationUserServiceTests
    {
        private readonly Mock<IApplicationUserRepository> repository = new Mock<IApplicationUserRepository>();
        private readonly Mock<IPasswordEncryptHandler> passwordEncryptHandler = new Mock<IPasswordEncryptHandler>();

        [Theory(DisplayName = "Should check contract")]
        [InlineData("", "abcdefg")]
        [InlineData("email@email.com", "")]
        [InlineData(null, "abcdefg")]
        [InlineData("email@email.com", null)]
        [InlineData("    ", "abcdefg")]
        [InlineData("email@email.com", "    ")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("    ", "    ")]
        public void TestGetUser_Contract(string email, string password)
        {
            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);

            Assert.Throws<ArgumentException>(() => service.GetUser(email, password));
        }

        [Fact(DisplayName = "Can return user info when user exist")]
        public void TestGetUser_Exist()
        {
            repository.Setup(x => x.GetUser("email@email.com"))
                      .Returns(new ApplicationUser()
                      {
                          Id = Guid.NewGuid(),
                          Email = "email@email.com",
                          Password = "encryptedPassword",
                          NickName = "BigEgg",
                          RegisterSource = RegisterSourceType.Website,
                          Role = UserRole.User,
                          CreatedTime = DateTime.UtcNow,
                          LastUpdated = DateTime.UtcNow
                      });

            passwordEncryptHandler.Setup(x => x.ComparePassword("password", "encryptedPassword"))
                                  .Returns(true);

            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);
            var user = service.GetUser("email@email.com", "password");

            Assert.NotNull(user);

            repository.Reset();
            passwordEncryptHandler.Reset();
        }

        [Fact(DisplayName = "Can not return user info when user not exist")]
        public void TestGetUser_NotExist()
        {
            repository.Setup(x => x.GetUser("email@email.com"))
                      .Returns<ApplicationUser>(null);

            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);
            var user = service.GetUser("email@email.com", "password");

            Assert.Null(user);

            repository.Reset();
        }

        [Fact(DisplayName = "Can return user info when password not matched")]
        public void TestGetUser_PasswordNotMatch()
        {
            repository.Setup(x => x.GetUser("email@email.com"))
                      .Returns(new ApplicationUser()
                      {
                          Id = Guid.NewGuid(),
                          Email = "email@email.com",
                          Password = "encryptPassword",
                          NickName = "BigEgg",
                          RegisterSource = RegisterSourceType.Website,
                          Role = UserRole.User,
                          CreatedTime = DateTime.UtcNow,
                          LastUpdated = DateTime.UtcNow
                      });

            passwordEncryptHandler.Setup(x => x.ComparePassword("password", "encryptedPassword"))
                                  .Returns(false);

            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);
            var user = service.GetUser("email@email.com", "password");

            Assert.Null(user);

            repository.Reset();
            passwordEncryptHandler.Reset();
        }

        [Theory(DisplayName = "Should check contract")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public void TestIsEmailAlreadyUsed_Contract(string email)
        {
            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);

            Assert.Throws<ArgumentException>(() => service.IsEmailAlreadyUsed(email));
        }

        [Fact(DisplayName = "Can return user info when user exist")]
        public void TestIsEmailAlreadyUsed_Exist()
        {
            repository.Setup(x => x.GetUser("email@email.com"))
                      .Returns(new ApplicationUser()
                      {
                          Id = Guid.NewGuid(),
                          Email = "email@email.com",
                          Password = "encryptedPassword",
                          NickName = "BigEgg",
                          RegisterSource = RegisterSourceType.Website,
                          Role = UserRole.User,
                          CreatedTime = DateTime.UtcNow,
                          LastUpdated = DateTime.UtcNow
                      });

            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);

            Assert.True(service.IsEmailAlreadyUsed("email@email.com"));

            repository.Reset();
        }

        [Fact(DisplayName = "Can not return user info when user not exist")]
        public void TestIsEmailAlreadyUsed_NotExist()
        {
            repository.Setup(x => x.GetUser("email@email.com"))
                      .Returns<ApplicationUser>(null);

            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);

            Assert.False(service.IsEmailAlreadyUsed("email@email.com"));

            repository.Reset();
        }

        [Theory(DisplayName = "Should check contract")]
        [InlineData("", "abcdefg", "BigEgg")]
        [InlineData("email@email.com", "", "BigEgg")]
        [InlineData("email@email.com", "abcdefg", "")]
        [InlineData(null, "abcdefg", "BigEgg")]
        [InlineData("email@email.com", null, "BigEgg")]
        [InlineData("email@email.com", "abcdefg", null)]
        [InlineData("    ", "abcdefg", "BigEgg")]
        [InlineData("email@email.com", "    ", "BigEgg")]
        [InlineData("email@email.com", "abcdefg", "    ")]
        [InlineData("", "", "")]
        [InlineData(null, null, null)]
        [InlineData("    ", "    ", "    ")]
        public void TestNewUser_Contract(string email, string password, string nickName)
        {
            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);

            Assert.Throws<ArgumentException>(() => service.NewUser(email, password, nickName, RegisterSourceType.Website));
        }

        [Fact(DisplayName = "Can successfully add new user")]
        public void TestNewUser()
        {
            repository.SetupSequence(x => x.GetUser("email@email.com"))
                      .Returns(null)
                      .Returns(new ApplicationUser()
                      {
                          Id = Guid.NewGuid(),
                          Email = "email@email.com",
                          Password = "encryptedPassword",
                          NickName = "BigEgg",
                          RegisterSource = RegisterSourceType.Website,
                          Role = UserRole.User,
                          CreatedTime = DateTime.UtcNow,
                          LastUpdated = DateTime.UtcNow
                      });
            passwordEncryptHandler.Setup(x => x.EncryptPassword("password"))
                                  .Returns("encryptedPassword");

            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);

            var user = service.NewUser("email@email.com", "password", "BigEgg", RegisterSourceType.Website);

            Assert.NotNull(user);
            repository.Verify(x => x.AddNewUser("email@email.com", "encryptedPassword", "BigEgg", RegisterSourceType.Website));

            repository.Reset();
            passwordEncryptHandler.Reset();
        }

        [Fact(DisplayName = "Throw exception when email already existed")]
        public void TestNewUser_EmailExisted()
        {
            repository.Setup(x => x.GetUser("email@email.com"))
                      .Returns(new ApplicationUser()
                      {
                          Id = Guid.NewGuid(),
                          Email = "email@email.com",
                          Password = "encryptedPassword",
                          NickName = "BigEgg",
                          RegisterSource = RegisterSourceType.Website,
                          Role = UserRole.User,
                          CreatedTime = DateTime.UtcNow,
                          LastUpdated = DateTime.UtcNow
                      });

            var service = new ApplicationUserService(repository.Object, passwordEncryptHandler.Object);
            Assert.Throws<InvalidOperationException>(()=> service.NewUser("email@email.com", "password", "BigEgg", RegisterSourceType.Website));

            repository.Reset();
        }
    }
}
