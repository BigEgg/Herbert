namespace Herbert.Services.Tests.Access
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Caching.Memory;

    using Moq;
    using Xunit;

    using Herbert.DAL.Repositories.Interfaces;
    using Herbert.Models.Access;
    using Herbert.Services.Access;

    public class SupportApplicationServiceTests
    {
        private readonly Mock<ISupportApplicationRepository> repository = new Mock<ISupportApplicationRepository>();
        private const string SUPPORT_APPLICATION_CACHE_KEY = "SUPPORT_APPLICATION_CACHE_KEY";

        [Theory(DisplayName = "Should check contract")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public void TestGetApplicationType_Contract(string appSecret)
        {
            using (var cache = new MemoryCache(new MemoryCacheOptions()))
            {
                var service = new SupportApplicationService(repository.Object, cache);
                Assert.Throws<ArgumentException>(() => service.GetApplicationType(Guid.NewGuid(), appSecret));
            }
        }

        [Fact(DisplayName = "Should get application type when app id and secret match")]
        public void TestGetApplicationType_Match_NotInCache()
        {
            repository.Setup(x => x.GetAll())
                      .Returns(new List<SupportApplication>()
                      {
                          new SupportApplication()
                          {
                               AppId = new Guid("721a75ab-65e6-420a-a60b-0f32b758d725"),
                               AppSecret = "Q46LTjtZQY8XroplIQv2Uet0L1z0iFbpkRBF1yIVVOCjbthjIfwbGbaQvBK31fxh2",
                               ApplicationType = SupportApplicationType.Website,
                               CreatedTime = DateTime.UtcNow,
                               LastUpdated = DateTime.UtcNow
                          }
                      });

            using (var cache = new MemoryCache(new MemoryCacheOptions()))
            {
                var service = new SupportApplicationService(repository.Object, cache);

                var type = service.GetApplicationType(
                    new Guid("721a75ab-65e6-420a-a60b-0f32b758d725"),
                    "Q46LTjtZQY8XroplIQv2Uet0L1z0iFbpkRBF1yIVVOCjbthjIfwbGbaQvBK31fxh2");

                Assert.True(type.HasValue);
                Assert.Equal(SupportApplicationType.Website, type.Value);

                var dataInCache = cache.Get<IList<SupportApplication>>(SUPPORT_APPLICATION_CACHE_KEY);
                Assert.NotNull(dataInCache);
                Assert.Equal("721a75ab-65e6-420a-a60b-0f32b758d725", dataInCache.First().AppId.ToString());
            }
        }

        [Fact(DisplayName = "Should get null when app id and secret not match")]
        public void TestGetApplicationType_NotMatch_NotInCache()
        {
            repository.Setup(x => x.GetAll())
                      .Returns(new List<SupportApplication>()
                      {
                          new SupportApplication()
                          {
                               AppId = new Guid("721a75ab-65e6-420a-a60b-0f32b758d725"),
                               AppSecret = "Q46LTjtZQY8XroplIQv2Uet0L1z0iFbpkRBF1yIVVOCjbthjIfwbGbaQvBK31fxh2",
                               ApplicationType = SupportApplicationType.Website,
                               CreatedTime = DateTime.UtcNow,
                               LastUpdated = DateTime.UtcNow
                          }
                      });

            using (var cache = new MemoryCache(new MemoryCacheOptions()))
            {
                var service = new SupportApplicationService(repository.Object, cache);

                var type = service.GetApplicationType(
                    new Guid("721a75ab-65e6-420a-a60b-0f32b758d726"),
                    "Q46LTjtZQY8XroplIQv2Uet0L1z0iFbpkRBF1yIVVOCjbthjIfwbGbaQvBK31fxh2");

                Assert.False(type.HasValue);

                var dataInCache = cache.Get<IList<SupportApplication>>(SUPPORT_APPLICATION_CACHE_KEY);
                Assert.NotNull(dataInCache);
                Assert.Equal("721a75ab-65e6-420a-a60b-0f32b758d725", dataInCache.First().AppId.ToString());
            }
        }

        [Fact(DisplayName = "Should get application type when app id and secret match")]
        public void TestGetApplicationType_Match_InCache()
        {
            using (var cache = new MemoryCache(new MemoryCacheOptions()))
            {
                cache.Set(
                    SUPPORT_APPLICATION_CACHE_KEY, 
                    new List<SupportApplication>()
                    {
                        new SupportApplication()
                        {
                             AppId = new Guid("721a75ab-65e6-420a-a60b-0f32b758d725"),
                             AppSecret = "Q46LTjtZQY8XroplIQv2Uet0L1z0iFbpkRBF1yIVVOCjbthjIfwbGbaQvBK31fxh2",
                             ApplicationType = SupportApplicationType.Website,
                             CreatedTime = DateTime.UtcNow,
                             LastUpdated = DateTime.UtcNow
                        }
                    });

                var service = new SupportApplicationService(repository.Object, cache);

                var type = service.GetApplicationType(
                    new Guid("721a75ab-65e6-420a-a60b-0f32b758d725"),
                    "Q46LTjtZQY8XroplIQv2Uet0L1z0iFbpkRBF1yIVVOCjbthjIfwbGbaQvBK31fxh2");

                Assert.True(type.HasValue);
                Assert.Equal(SupportApplicationType.Website, type.Value);
                repository.Verify(x => x.GetAll(), Times.Never);
            }
        }

        [Fact(DisplayName = "Should get null when app id and secret not match")]
        public void TestGetApplicationType_NotMatch_InCache()
        {
            using (var cache = new MemoryCache(new MemoryCacheOptions()))
            {
                cache.Set(
                    SUPPORT_APPLICATION_CACHE_KEY,
                    new List<SupportApplication>()
                    {
                        new SupportApplication()
                        {
                             AppId = new Guid("721a75ab-65e6-420a-a60b-0f32b758d725"),
                             AppSecret = "Q46LTjtZQY8XroplIQv2Uet0L1z0iFbpkRBF1yIVVOCjbthjIfwbGbaQvBK31fxh2",
                             ApplicationType = SupportApplicationType.Website,
                             CreatedTime = DateTime.UtcNow,
                             LastUpdated = DateTime.UtcNow
                        }
                    });

                var service = new SupportApplicationService(repository.Object, cache);

                var type = service.GetApplicationType(
                    new Guid("721a75ab-65e6-420a-a60b-0f32b758d726"),
                    "Q46LTjtZQY8XroplIQv2Uet0L1z0iFbpkRBF1yIVVOCjbthjIfwbGbaQvBK31fxh2");

                Assert.False(type.HasValue);
                repository.Verify(x => x.GetAll(), Times.Never);
            }
        }
    }
}
