using KingPriceDemo.Domain.Entities;
using KingPriceDemo.Domain.Enums;
using KingPriceDemo.Persistence.Common.Repositories.Enums;
using KingPriceDemo.Persistence.Data.Context;
using KingPriceDemo.Persistence.Locks;

namespace KingPriceDemo.Persistence.UnitTests.Tests.Locks
{
    public class GroupInviteLockTests
    {
        private readonly Mock<KingPriceContext> _mockContext;
        private readonly GroupInviteLock _lock;

        public GroupInviteLockTests()
        {
            _mockContext = new Mock<KingPriceContext>();
            _lock = new GroupInviteLock(_mockContext.Object);
        }

        [Fact]
        public async Task HasAccess_WhenUpdateOperation_ShouldReturnFalse()
        {
            // Arrange
            var identityId = 1;
            var groupInvite = new GroupInvite { GroupId = 1 };

            // Act
            var result = await _lock.HasAccess(groupInvite, identityId, RepositoryOperationEnum.Update, GroupRightsEnum.Owner, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenDeleteOperation_ShouldReturnFalse()
        {
            // Arrange
            var identityId = 1;
            var groupInvite = new GroupInvite { GroupId = 1 };

            // Act
            var result = await _lock.HasAccess(groupInvite, identityId, RepositoryOperationEnum.Delete, GroupRightsEnum.Owner, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenGroupIdIsZero_ShouldReturnFalse()
        {
            // Arrange
            var identityId = 1;
            var groupInvite = new GroupInvite { GroupId = 0 };

            // Act
            var result = await _lock.HasAccess(groupInvite, identityId, RepositoryOperationEnum.Insert, GroupRightsEnum.ReadWrite, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenUserDoesNotHaveRights_ShouldReturnFalse()
        {
            // Arrange
            var identityId = 1;
            var groupInvite = new GroupInvite { GroupId = 1 };

            var groupList = new List<UserGroup>
            {
                new UserGroup { UserId = 1, GroupId = 1, Rights = GroupRightsEnum.Read }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(groupList.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _lock.HasAccess(groupInvite, identityId, RepositoryOperationEnum.Insert, GroupRightsEnum.ReadWrite, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenUserHasRights_ShouldReturnTrue()
        {
            // Arrange
            var identityId = 1;
            var groupInvite = new GroupInvite { GroupId = 1 };

            var groupList = new List<UserGroup>
            {
                new UserGroup { UserId = 1, GroupId = 1, Rights = GroupRightsEnum.ReadWrite }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(groupList.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _lock.HasAccess(groupInvite, identityId, RepositoryOperationEnum.Insert, GroupRightsEnum.ReadWrite, default);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Secured_WhenUserHasRights_ShouldReturnOne()
        {
            // Arrange
            var identityId = 1;
            var requirment = GroupRightsEnum.Read;

            var groupInviteList = new List<GroupInvite>
            {
                new GroupInvite { Id = Guid.CreateVersion7(), GroupId = 1 }
            };

            _mockContext.Setup(x => x.Set<GroupInvite>()).Returns(groupInviteList.AsQueryable().BuildMockDbSet().Object);

            var groupList = new List<UserGroup>
            {
                new UserGroup { UserId = 1, GroupId = 1, Rights = GroupRightsEnum.ReadWrite }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(groupList.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = _lock.Secured(identityId, requirment);
            // Assert
            result.Should().HaveCount(1);
        }

        [Fact]
        public void Secured_WhenUserHasRightsAndInviteNotInGroup_ShouldReturnZero()
        {
            // Arrange
            var identityId = 1;
            var requirment = GroupRightsEnum.Read;

            var groupInviteList = new List<GroupInvite>
            {
                new GroupInvite { Id = Guid.CreateVersion7(), GroupId = 2 }
            };

            _mockContext.Setup(x => x.Set<GroupInvite>()).Returns(groupInviteList.AsQueryable().BuildMockDbSet().Object);

            var groupList = new List<UserGroup>
            {
                new UserGroup { UserId = 1, GroupId = 1, Rights = GroupRightsEnum.ReadWrite }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(groupList.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = _lock.Secured(identityId, requirment);
            // Assert
            result.Should().HaveCount(0);
        }
    }
}
