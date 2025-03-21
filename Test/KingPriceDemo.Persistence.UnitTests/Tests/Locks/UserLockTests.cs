using KingPriceDemo.Domain.Entities;
using KingPriceDemo.Domain.Enums;
using KingPriceDemo.Persistence.Common.Repositories.Enums;
using KingPriceDemo.Persistence.Data.Context;
using KingPriceDemo.Persistence.Locks;

namespace KingPriceDemo.Persistence.UnitTests.Tests.Locks
{
    public class UserLockTests
    {
        private readonly Mock<KingPriceContext> _mockContext;
        private readonly UserLock _lock;

        public UserLockTests()
        {
            _mockContext = new Mock<KingPriceContext>();
            _lock = new UserLock(_mockContext.Object);
        }

        [Fact]
        public async Task HasAccess_WhenInsert_ShouldReturnTrue()
        {
            // Arrange
            var user = new User();
            var identityId = 1;
            var operation = RepositoryOperationEnum.Insert;
            var requirement = GroupRightsEnum.Read;

            // Act
            var result = await _lock.HasAccess(user, identityId, operation, requirement, default);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task HasAccess_WhenUserIdIsZero_ShouldReturnFalse()
        {
            // Arrange
            var user = new User { Id = 0 };
            var identityId = 1;
            var operation = RepositoryOperationEnum.Update;
            var requirement = GroupRightsEnum.Read;

            // Act
            var result = await _lock.HasAccess(user, identityId, operation, requirement, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenUserIdIsNotIdentityId_ShouldReturnFalse()
        {
            // Arrange
            var user = new User { Id = 2 };
            var identityId = 1;
            var operation = RepositoryOperationEnum.Update;
            var requirement = GroupRightsEnum.Read;

            // Act
            var result = await _lock.HasAccess(user, identityId, operation, requirement, default);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task HasAccess_WhenUserIdIsIdentityId_ShouldReturnTrue()
        {
            // Arrange
            var user = new User { Id = 1 };
            var identityId = 1;
            var operation = RepositoryOperationEnum.Update;
            var requirement = GroupRightsEnum.Read;

            // Act
            var result = await _lock.HasAccess(user, identityId, operation, requirement, default);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Secured_WhenUserNotInGroup_ShouldReturnUser()
        {
            // Arrange
            var identityId = 1;
            var requirement = GroupRightsEnum.Read;

            var userList = new List<User>
            {
                new User
                {
                    Id = identityId
                }
            };

            _mockContext.Setup(x => x.Set<User>()).Returns(() => userList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            var userGroupList = new List<UserGroup>
            {
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(() => userGroupList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            // Act
            var result = _lock.Secured(identityId, requirement);

            // Assert
            result.Should().HaveCount(1);
        }

        [Fact]
        public void Secured_WhenUserInGroup_ShouldReturnUser()
        {
            // Arrange
            var identityId = 1;
            var requirement = GroupRightsEnum.Read;

            var userList = new List<User>
            {
                new User
                {
                    Id = identityId,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            UserId = identityId,
                            GroupId = 1,
                            Rights = GroupRightsEnum.Read
                        }
                    }
                }
            };

            _mockContext.Setup(x => x.Set<User>()).Returns(() => userList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            var userGroupList = new List<UserGroup>
            {
                new UserGroup
                {
                    UserId = identityId,
                    GroupId = 1,
                    Rights = GroupRightsEnum.Read
                }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(() => userGroupList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            // Act
            var result = _lock.Secured(identityId, requirement);

            // Assert
            result.Should().HaveCount(1);
        }

        [Fact]
        public void Secured_WhenUserInGroupWithMultipleUsers_ShouldReturnMultipleUser()
        {
            // Arrange
            var identityId = 1;
            var requirement = GroupRightsEnum.Read;

            var userList = new List<User>
            {
                new User
                {
                    Id = identityId,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            UserId = identityId,
                            GroupId = 1,
                            Rights = GroupRightsEnum.Read
                        }
                    }
                },
                new User
                {
                    Id = 2,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            UserId = 2,
                            GroupId = 1,
                            Rights = GroupRightsEnum.Read
                        }
                    }
                }
            };

            _mockContext.Setup(x => x.Set<User>()).Returns(() => userList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            var userGroupList = new List<UserGroup>
            {
                new UserGroup
                {
                    UserId = identityId,
                    GroupId = 1,
                    Rights = GroupRightsEnum.Read
                },
                new UserGroup
                {
                    UserId = 2,
                    GroupId = 1,
                    Rights = GroupRightsEnum.Read
                }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(() => userGroupList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            // Act
            var result = _lock.Secured(identityId, requirement);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public void Secured_WhenUserInGroupWithOtherUsersInDifferentGroups_ShouldReturnUser()
        {
            // Arrange
            var identityId = 1;
            var requirement = GroupRightsEnum.Read;

            var userList = new List<User>
            {
                new User
                {
                    Id = identityId,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            UserId = identityId,
                            GroupId = 1,
                            Rights = GroupRightsEnum.Read
                        }
                    }
                },
                new User
                {
                    Id = 2,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            UserId = 2,
                            GroupId = 2,
                            Rights = GroupRightsEnum.Read
                        }
                    }
                }
            };

            _mockContext.Setup(x => x.Set<User>()).Returns(() => userList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            var userGroupList = new List<UserGroup>
            {
                new UserGroup
                {
                    UserId = identityId,
                    GroupId = 1,
                    Rights = GroupRightsEnum.Read
                },
                new UserGroup
                {
                    UserId = 2,
                    GroupId = 2,
                    Rights = GroupRightsEnum.Read
                }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(() => userGroupList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            // Act
            var result = _lock.Secured(identityId, requirement);

            // Assert
            result.Should().HaveCount(1);
        }

        [Fact]
        public void Secured_WhenUserInGroupWithNoWightsWithMultipleUsers_ShouldReturnOnlyIdentityUser()
        {
            // Arrange
            var identityId = 1;
            var requirement = GroupRightsEnum.Read;

            var userList = new List<User>
            {
                new User
                {
                    Id = identityId,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            UserId = identityId,
                            GroupId = 1,
                            Rights = GroupRightsEnum.None
                        }
                    }
                },
                new User
                {
                    Id = 2,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            UserId = 2,
                            GroupId = 1,
                            Rights = GroupRightsEnum.Read
                        }
                    }
                }
            };

            _mockContext.Setup(x => x.Set<User>()).Returns(() => userList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            var userGroupList = new List<UserGroup>
            {
                new UserGroup
                {
                    UserId = identityId,
                    GroupId = 1,
                    Rights = GroupRightsEnum.None
                },
                new UserGroup
                {
                    UserId = 2,
                    GroupId = 1,
                    Rights = GroupRightsEnum.Read
                }
            };

            _mockContext.Setup(x => x.Set<UserGroup>()).Returns(() => userGroupList
                .AsQueryable()
                .BuildMockDbSet()
                .Object
            );

            // Act
            var result = _lock.Secured(identityId, requirement);

            // Assert
            result.Should().HaveCount(1);
        }
    }
}
