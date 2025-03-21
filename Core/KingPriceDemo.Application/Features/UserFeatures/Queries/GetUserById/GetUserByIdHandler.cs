using KingPriceDemo.Application.Repositories.QueryRepositories;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.UserFeatures.Queries.GetUserById
{
    public class GetUserByIdHandler(IUserQueryRepository repo)
        : IQueryHandler<GetUserByIdRequest, GetUserByIdResponse>
    {
        public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await repo.Users
                .Where(u => u.Id == request.Id)
                .Select(u => new GetUserByIdResponse
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Surname = u.Surname,
                    Email = u.Email,
                    CellphoneNumber = u.CellphoneNumber
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                throw new NotFoundException(nameof(User), request.Id);

            return user;
        }
    }
}
