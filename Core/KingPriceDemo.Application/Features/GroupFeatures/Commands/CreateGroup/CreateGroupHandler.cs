using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.CommandRepositories;
using KingPriceDemo.Domain.Enums;

namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.CreateGroup
{
    public class CreateGroupHandler(IIdentityInfo identityInfo, IGroupCommandRepository repo)
        : ICommandHandler<CreateGroupRequest, CreateGroupResponse>
    {
        public async Task<CreateGroupResponse> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            var group = new Group
            {
                Name = request.Name,
                UserGroups = new List<UserGroup>
                {
                    new UserGroup
                    {
                        UserId = identityInfo.GetIdentityId(),
                        Rights = GroupRightsEnum.Owner
                    }
                }
            };

            await repo.InsertAsync(group, true, cancellationToken);

            return new CreateGroupResponse(group.Id);
        }
    }
}
