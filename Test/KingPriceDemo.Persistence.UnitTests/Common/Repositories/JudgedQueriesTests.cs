using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Domain.Enums;
using KingPriceDemo.Persistence.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace KingPriceDemo.Persistence.UnitTests.Common.Repositories
{
    public class DummyEntity { }

    public class JudgedQueriesFixture
    {
        public DbContext ContextMock { get; set; }
        public List<DummyEntity> DummyEntities { get; set; }
        public DbSet<DummyEntity> DummyDbSet { get; set; }

        public JudgedQueriesFixture()
        {
            DummyEntities = new List<DummyEntity>
            {
                new DummyEntity(),
                new DummyEntity()
            };

            var dummyDbSetMock = DummyEntities.AsQueryable().BuildMockDbSet();
            DummyDbSet = dummyDbSetMock.Object;

            var dbContextMock = new Mock<DbContext>(new DbContextOptions<DbContext>());
            dbContextMock.Setup(x => x.Set<DummyEntity>()).Returns(() => DummyDbSet);

            ContextMock = dbContextMock.Object;
        }
    }

    public class JudgedQueriesTests : IClassFixture<JudgedQueriesFixture>
    {
        private readonly DbContext _context;

        public JudgedQueriesTests(JudgedQueriesFixture fixture)
        {
            _context = fixture.ContextMock;
        }


        [Fact]
        public void Secure_WhenSuperAdmin_ShouldReturnContextSet()
        {
            // Arrange
            var identityMock = new Mock<IIdentityInfo>();
            identityMock.Setup(x => x.HasRole(ApplicationRoleEnum.SuperAdmin)).Returns(true);

            // Use a real instance of AccessRequirements.
            var requirements = new AccessRequirements();

            // No protection needed for SuperAdmin.
            var protections = new List<IProtected>();

            var judgedQueries = new JudgedQueries<DbContext>(
                _context,
                identityMock.Object,
                requirements,
                protections);

            // Act
            var result = judgedQueries.Secure<DummyEntity>();

            // Assert
            // The result should be the same instance returned by DbContext.Set<T>().
            var expected = _context.Set<DummyEntity>();
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void Secure_WhenProtectionMatches_ShouldReturnSecuredResult()
        {
            // Arrange
            var securedQueryable = new List<DummyEntity>
            {
                new DummyEntity()
            }.AsQueryable();

            var identityMock = new Mock<IIdentityInfo>();
            identityMock.Setup(x => x.HasRole(ApplicationRoleEnum.SuperAdmin)).Returns(false);
            identityMock.Setup(x => x.GetIdentityId()).Returns(100);

            var requirements = new AccessRequirements();
            var currentRequirement = requirements.GetRequirment();

            var protectedMock = new Mock<IProtected<DummyEntity>>();
            protectedMock.Setup(x => x.IsMatch(typeof(DummyEntity))).Returns(true);
            protectedMock.Setup(x => x.Secured(100, currentRequirement)).Returns(securedQueryable);

            var protections = new List<IProtected> { protectedMock.Object };

            var judgedQueries = new JudgedQueries<DbContext>(
                _context,
                identityMock.Object,
                requirements,
                protections);

            // Act
            var result = judgedQueries.Secure<DummyEntity>();

            // Assert
            result.Should().BeSameAs(securedQueryable);
        }

        [Fact]
        public void Secure_WhenNoProtectionMatches_ShouldReturnContextSet()
        {
            // Arrange
            var identityMock = new Mock<IIdentityInfo>();
            identityMock.Setup(x => x.HasRole(ApplicationRoleEnum.SuperAdmin)).Returns(false);

            var requirements = new AccessRequirements();

            var protectedMock = new Mock<IProtected<DummyEntity>>();
            protectedMock.Setup(x => x.IsMatch(typeof(DummyEntity))).Returns(false);
            var protections = new List<IProtected> { protectedMock.Object };

            var judgedQueries = new JudgedQueries<DbContext>(
                _context,
                identityMock.Object,
                requirements,
                protections);

            // Act
            var result = judgedQueries.Secure<DummyEntity>();

            // Assert
            var expected = _context.Set<DummyEntity>();
            result.Should().BeSameAs(expected);
        }
    }
}
