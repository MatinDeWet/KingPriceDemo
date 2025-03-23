using KingPriceDemo.Application.Common.Pagination;
using KingPriceDemo.Application.Common.Pagination.Models;
using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.QueryRepositories;

namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.SearchGroup
{
    public class SearchGroupHandler(IIdentityInfo identityInfo, IGroupQueryRepository repo)
        : IQueryHandler<SearchGroupRequest, PageableResponse<SearchGroupResponse>>
    {
        public async Task<PageableResponse<SearchGroupResponse>> Handle(SearchGroupRequest request, CancellationToken cancellationToken)
        {
            var groups = await repo.Groups
                .Select(g => new SearchGroupResponse
                {
                    Id = g.Id,
                    Name = g.Name,
                    UserCount = g.UserGroups.Count,
                    GroupRights = g.UserGroups
                        .Where(ug => ug.UserId == identityInfo.GetIdentityId())
                        .Select(ug => ug.Rights)
                        .FirstOrDefault()
                })
                .ToPageableListAsync(x => x.Id, request, cancellationToken);

            return groups;
        }
    }
}
