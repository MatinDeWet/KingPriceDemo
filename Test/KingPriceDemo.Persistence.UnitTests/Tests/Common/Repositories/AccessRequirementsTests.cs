using KingPriceDemo.Domain.Enums;
using KingPriceDemo.Persistence.Common.Repositories;

namespace KingPriceDemo.Persistence.UnitTests.Tests.Common.Repositories
{
    public class AccessRequirementsTests
    {
        [Fact]
        public void Constructor_WhenCalled_ShouldResetToDefault()
        {
            // Arrange

            // Act
            var accessRequirements = new AccessRequirements();

            // Assert
            accessRequirements.GetRequirment().Should().Be(GroupRightsEnum.Read);
            accessRequirements.IsSet.Should().BeFalse();
        }

        [Fact]
        public void SetRequirement_WhenCalled_WithValidValue_ShouldSetRequirementAndMarkIsSetTrue()
        {
            // Arrange
            var accessRequirements = new AccessRequirements();

            // Act
            accessRequirements.SetRequirement(GroupRightsEnum.ReadWrite);

            // Assert
            accessRequirements.GetRequirment().Should().Be(GroupRightsEnum.ReadWrite);
            accessRequirements.IsSet.Should().BeTrue();
        }

        [Fact]
        public void SetRequirement_WhenCalled_WithNone_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var accessRequirements = new AccessRequirements();

            // Act
            Action act = () => accessRequirements.SetRequirement(GroupRightsEnum.None);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Access Requirement 'None' is invalid");
        }

        [Fact]
        public void Reset_WhenCalled_ShouldSetRequirementToReadAndMarkIsSetFalse()
        {
            // Arrange
            var accessRequirements = new AccessRequirements();

            accessRequirements.SetRequirement(GroupRightsEnum.ReadWrite);
            accessRequirements.IsSet.Should().BeTrue();

            // Act
            accessRequirements.Reset();

            // Assert
            accessRequirements.GetRequirment().Should().Be(GroupRightsEnum.Read);
            accessRequirements.IsSet.Should().BeFalse();
        }
    }
}
