using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.CommandRepositories;
using KingPriceDemo.Application.Repositories.QueryRepositories;
using KingPriceDemo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.JoinGroupWithToken
{
    public class JoinGroupWithTokenHandler(IIdentityInfo identityInfo, IGroupQueryRepository queryRepo, IGroupCommandRepository commandRepo)
        : ICommandHandler<JoinGroupWithTokenRequest>
    {
        public async Task<Unit> Handle(JoinGroupWithTokenRequest request, CancellationToken cancellationToken)
        {
            var group = await queryRepo.UnlockedGroups
                .Where(g => g.InviteToken == request.Token)
                .FirstOrDefaultAsync(cancellationToken);

            if (group is null)
                throw new NotFoundException(nameof(Group), request.Token);

            var userId = identityInfo.GetIdentityId();

            var alreadyJoined = await queryRepo.UserGroups
                .Where(ug => ug.GroupId == group.Id && ug.UserId == userId)
                .AnyAsync(cancellationToken);

            if (alreadyJoined)
                throw new BadRequestException("User already joined this group");

            var userGroup = new UserGroup
            {
                GroupId = group.Id,
                UserId = userId,
                Rights = GroupRightsEnum.Read
            };

            await commandRepo.InsertAsync(userGroup, true, cancellationToken);

            return Unit.Value;
        }
    }
}
