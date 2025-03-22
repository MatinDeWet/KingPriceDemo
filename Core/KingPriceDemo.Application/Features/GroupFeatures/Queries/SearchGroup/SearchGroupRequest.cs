using KingPriceDemo.Application.Common.Pagination.Models;

namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.SearchGroup
{
    public class SearchGroupRequest : PageableRequest, IQuery<PageableResponse<SearchGroupResponse>>
    {
    }
}
