using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.CommandRepositories;
using KingPriceDemo.Application.Repositories.QueryRepositories;
using KingPriceDemo.Domain.Enums;
using KingPriceDemo.Domain.EventEntities;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.GroupFeatures.Notifications.DeleteOwnerGroups
{
    public class DeleteUserGroupsHandler(IIdentityInfo identityInfo, IGroupQueryRepository queryRepo, IGroupCommandRepository commandRepo)
        : IPriorityNotificationHandler<DeleteUserEvent>
    {
        public int Priority => 1;

        public async Task Handle(DeleteUserEvent request, CancellationToken cancellationToken)
        {
            var userId = identityInfo.GetIdentityId();

            var groups = await queryRepo.UserGroups
                .Where(x => x.UserId == userId && x.Rights == GroupRightsEnum.Owner)
                .Select(x => x.Group)
                .ToListAsync(cancellationToken);

            foreach (var group in groups)
                await commandRepo.DeleteAsync(group, cancellationToken);

            await commandRepo.SaveAsync(cancellationToken);
        }
    }
}
