
using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Domain.Enums;
using System.Security.Claims;

namespace KingPriceDemo.UnitTests.Tests.Application.Common.Security
{
    public class IdentityInfoFixture
    {
        public IInfoSetter InfoSetter { get; }

        public IdentityInfoFixture()
        {
            InfoSetter = new InfoSetter();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "testuser@gmail.com"),
                new Claim(ClaimTypes.Role, ApplicationRoleEnum.Admin.ToString()),
                new Claim(ClaimTypes.Role, ApplicationRoleEnum.SuperAdmin.ToString()),
            };

            InfoSetter.SetUser(claims);
        }
    }

    public class IdentityInfoTests : IClassFixture<IdentityInfoFixture>
    {
        private readonly IdentityInfo _identityInfo;

        public IdentityInfoTests(IdentityInfoFixture fixture)
        {
            _identityInfo = new IdentityInfo(fixture.InfoSetter);
        }

        [Fact]
        public void GetIdentityId_WhenCalled_ShouldReturnIdentityId()
        {
            // Act
            var result = _identityInfo.GetIdentityId();

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public void HasRole_WhenCalled_WithSuperAdmin_TestAdmin_ShouldReturnTrue()
        {
            // Act
            var result = _identityInfo.HasRole(ApplicationRoleEnum.Admin);
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void HasRole_WhenCalled_WithSuperAdmin_TestSuperAdmin_ShouldReturnTrue()
        {
            // Act
            var result = _identityInfo.HasRole(ApplicationRoleEnum.SuperAdmin);
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GetValue_WhenCalled_ShouldReturnValue()
        {
            // Act
            var result = _identityInfo.GetValue(ClaimTypes.Email);
            // Assert
            result.Should().Be("testuser@gmail.com");
        }

        [Fact]
        public void HasValue_WhenCalled_ShouldReturnTrue()
        {
            // Act
            var result = _identityInfo.HasValue(ClaimTypes.Email);
            // Assert
            result.Should().BeTrue();
        }
    }
}
