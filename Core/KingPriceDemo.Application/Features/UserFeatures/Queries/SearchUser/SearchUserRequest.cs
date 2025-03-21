using KingPriceDemo.Application.Common.Pagination.Models;

namespace KingPriceDemo.Application.Features.UserFeatures.Queries.SearchUser
{
    public class SearchUserRequest : PageableRequest, IQuery<PageableResponse<SearchUserResponse>>
    {
    }
}
