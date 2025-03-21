using KingPriceDemo.Application.Common.Pagination;
using KingPriceDemo.Application.Common.Pagination.Models;
using KingPriceDemo.Application.Repositories.QueryRepositories;

namespace KingPriceDemo.Application.Features.UserFeatures.Queries.SearchUser
{
    public class SearchUserHandler(IUserQueryRepository repo)
        : IQueryHandler<SearchUserRequest, PageableResponse<SearchUserResponse>>
    {
        public async Task<PageableResponse<SearchUserResponse>> Handle(SearchUserRequest request, CancellationToken cancellationToken)
        {
            var users = await repo.Users
                .Select(u => new SearchUserResponse
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Surname = u.Surname,
                    Email = u.Email,
                    CellphoneNumber = u.CellphoneNumber
                })
                .ToPageableListAsync(x => x.Id, request, cancellationToken);

            return users;
        }
    }
}
