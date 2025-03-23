using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.QueryRepositories;
using KingPriceDemo.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.GetGroupById
{
    public class GetGroupByIdHandler(IIdentityInfo identityInfo, IGroupQueryRepository repo)
        : IQueryHandler<GetGroupByIdRequest, GetGroupByIdResponse>
    {
        public async Task<GetGroupByIdResponse> Handle(GetGroupByIdRequest request, CancellationToken cancellationToken)
        {
            var group = await repo.Groups
                .Where(g => g.Id == request.Id)
                .Select(g => new GetGroupByIdResponse
                {
                    Id = g.Id,
                    Name = g.Name,
                    InviteToken = g.UserGroups.Any(ug => ug.UserId == identityInfo.GetIdentityId() && ug.Rights == GroupRightsEnum.Owner) ? g.InviteToken : string.Empty,
                    GroupRights = g.UserGroups
                        .Where(ug => ug.UserId == identityInfo.GetIdentityId())
                        .Select(ug => ug.Rights)
                        .FirstOrDefault(),
                    Users = g.UserGroups.Select(u => new GroupUsers
                    {
                        Id = u.User.Id,
                        Email = u.User.Email,
                        Rights = u.Rights,
                        FullName = u.User.FullName,
                        Surname = u.User.Surname
                    })
                    .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (group is null)
                throw new NotFoundException(nameof(Group), request.Id);

            return group;
        }
    }
}
