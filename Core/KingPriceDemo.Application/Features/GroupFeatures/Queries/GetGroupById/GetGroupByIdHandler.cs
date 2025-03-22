using KingPriceDemo.Application.Repositories.QueryRepositories;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.GetGroupById
{
    public class GetGroupByIdHandler(IGroupQueryRepository repo)
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
                    Users = g.UserGroups.Select(u => new GroupUsers
                    {
                        Id = u.User.Id,
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
