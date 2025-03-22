using KingPriceDemo.Application.Common.Pagination;
using KingPriceDemo.Application.Common.Pagination.Models;
using KingPriceDemo.Application.Repositories.QueryRepositories;

namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.SearchGroup
{
    public class SearchGroupHandler(IGroupQueryRepository repo)
        : IQueryHandler<SearchGroupRequest, PageableResponse<SearchGroupResponse>>
    {
        public async Task<PageableResponse<SearchGroupResponse>> Handle(SearchGroupRequest request, CancellationToken cancellationToken)
        {
            var groups = await repo.Groups
                .Select(g => new SearchGroupResponse
                {
                    Id = g.Id,
                    Name = g.Name,
                    UserCount = g.UserGroups.Count
                })
                .ToPageableListAsync(x => x.Id, request, cancellationToken);

            return groups;
        }
    }
}
