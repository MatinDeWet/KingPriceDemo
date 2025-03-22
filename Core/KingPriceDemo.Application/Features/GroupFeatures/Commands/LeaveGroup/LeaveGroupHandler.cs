using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.CommandRepositories;
using KingPriceDemo.Application.Repositories.QueryRepositories;
using KingPriceDemo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.LeaveGroup
{
    public class LeaveGroupHandler(IIdentityInfo identityInfo, IGroupQueryRepository queryRepo, IGroupCommandRepository commandRepo)
        : ICommandHandler<LeaveGroupRequest>
    {
        public async Task<Unit> Handle(LeaveGroupRequest request, CancellationToken cancellationToken)
        {
            var userId = identityInfo.GetIdentityId();

            var userGroup = await queryRepo.UserGroups
                .Where(ug => ug.GroupId == request.Id && ug.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);

            if (userGroup is null)
                throw new NotFoundException(nameof(UserGroup), request.Id);

            if (userGroup.UserId == userId && userGroup.Rights == GroupRightsEnum.Owner)
                throw new BadRequestException("Owner cannot leave the group");

            await commandRepo.DeleteAsync(userGroup, true, cancellationToken);

            return Unit.Value;
        }
    }
}
