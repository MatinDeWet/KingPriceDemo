using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Domain.Enums;
using Moq;
using System.Security.Claims;

namespace KingPriceDemo.UnitTests.Tests.Application.Common.Security
{
    public class InfoSetterTests
    {
        [Fact]
        public void SetUser_WhenCalled_ShouldSetUser()
        {
            // Arrange
            var infoSetter = new InfoSetter();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };

            // Act
            infoSetter.SetUser(claims);

            // Assert
            infoSetter.Should().NotBeEmpty();
            infoSetter.Should().HaveCount(1);
            infoSetter.Should().Contain(claims);
        }

        [Fact]
        public void SetUser_WhenCalled_ShouldCallClear()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };

            var mockInfoSetter = new Mock<InfoSetter> { CallBase = true };

            // Act
            mockInfoSetter.Object.SetUser(claims);

            // Assert
            mockInfoSetter.Verify(x => x.Clear(), Times.Once);
        }

        [Fact]
        public void SetUser_WhenCalled_ShouldCallAddRange()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };

            var mockInfoSetter = new Mock<InfoSetter> { CallBase = true };

            // Act
            mockInfoSetter.Object.SetUser(claims);

            // Assert
            mockInfoSetter.Verify(x => x.AddRange(It.Is<IEnumerable<Claim>>(c => c.SequenceEqual(claims))), Times.Once);
        }
    }
}
