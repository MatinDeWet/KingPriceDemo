using KingPriceDemo.Domain.Entities;
using KingPriceDemo.Domain.Enums;
using KingPriceDemo.Persistence.Common.Repositories.Enums;
using KingPriceDemo.Persistence.Data.Context;
using KingPriceDemo.Persistence.Locks;

using Group = KingPriceDemo.Domain.Entities.Group;

namespace KingPriceDemo.Persistence.UnitTests.Tests.Locks
{
    public class GroupLockTests
    {
        private readonly Mock<KingPriceContext> _mockContext;
        private readonly GroupLock _userLock;

        public GroupLockTests()
        {
            _mockContext = new Mock<KingPriceContext>();
            _userLock = new GroupLock(_mockContext.Object);
        }

        [Fact]
        public async Task HasAccess_WhenInsertOperationAndUserIdIsZero_ShouldReturnFalse()
        {
            // Arrange
            var identityId = 1;
            var group = new Group { UserGroups = new List<UserGroup> { new UserGroup { UserId = 0 } } };

            // Act
            var result = await _userLock.HasAccess(group, identityId, RepositoryOperationEnum.Insert, GroupRightsEnum.Owner, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenInsertOperationAndUserIdIsNotZero_ShouldReturnTrue()
        {
            // Arrange
            var identityId = 1;
            var group = new Group { UserGroups = new List<UserGroup> { new UserGroup { UserId = 1 } } };

            // Act
            var result = await _userLock.HasAccess(group, identityId, RepositoryOperationEnum.Insert, GroupRightsEnum.Owner, default);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task HasAccess_WhenInsertOperationAndUserIdIsNotIdentityId_ShouldReturnFalse()
        {
            // Arrange
            var identityId = 1;
            var group = new Group { UserGroups = new List<UserGroup> { new UserGroup { UserId = 2 } } };

            // Act
            var result = await _userLock.HasAccess(group, identityId, RepositoryOperationEnum.Insert, GroupRightsEnum.Owner, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenOperationIsNotInsertAndGroupIdIsZero_ShouldReturnFalse()
        {
            // Arrange
            var identityId = 1;
            var group = new Group { Id = 0 };

            // Act
            var result = await _userLock.HasAccess(group, identityId, RepositoryOperationEnum.Update, GroupRightsEnum.Owner, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenOperationIsNotInsertAndUserGroupDoesNotHaveOwnerRights_ShouldReturnFalse()
        {
            // Arrange
            var identityId = 1;
            var group = new Group { Id = 1 };

            var groupList = new List<UserGroup> 
            {
                new UserGroup { UserId = identityId, GroupId = 1, Rights = GroupRightsEnum.ReadWrite }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(groupList.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _userLock.HasAccess(group, identityId, RepositoryOperationEnum.Update, GroupRightsEnum.Owner, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenOperationIsNotInsertAndUserGroupHasOwnerRights_ShouldReturnTrue()
        {
            // Arrange
            var identityId = 1;
            var group = new Group { Id = 1 };
            var groupList = new List<UserGroup>
            {
                new UserGroup { UserId = identityId, GroupId = 1, Rights = GroupRightsEnum.Owner }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(groupList.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _userLock.HasAccess(group, identityId, RepositoryOperationEnum.Update, GroupRightsEnum.Owner, default);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task HasAccess_WhenOperationIsNotInsertAndUserDoesNotHaveGroupRights_ShouldReturnFalse()
        {
            // Arrange
            var identityId = 1;
            var group = new Group { Id = 2 };
            var groupList = new List<UserGroup>
            {
                new UserGroup { UserId = identityId, GroupId = 1, Rights = GroupRightsEnum.Owner }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(groupList.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _userLock.HasAccess(group, identityId, RepositoryOperationEnum.Update, GroupRightsEnum.Owner, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Secured_ShouldReturnQueryableOfGroup()
        {
            // Arrange
            var identityId = 1;
            var requirement = GroupRightsEnum.Owner;
            var groupList = new List<Group>
            {
                new Group { Id = 1 },
                new Group { Id = 2 }
            };

            _mockContext.Setup(x => x.Set<Group>()).Returns(groupList.AsQueryable().BuildMockDbSet().Object);

            var userGroupList = new List<UserGroup>
            {
                new UserGroup { UserId = identityId, GroupId = 1, Rights = requirement }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(userGroupList.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = _userLock.Secured(identityId, requirement);

            // Assert
            result.Should().BeEquivalentTo(groupList.Where(x => x.Id == 1));
        }
    }
}
