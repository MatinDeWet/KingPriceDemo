using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.CommandRepositories;
using KingPriceDemo.Application.Repositories.QueryRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.UserFeatures.Commands.DeleteUser
{
    public class DeleteUserHandler(IIdentityInfo identityInfo, IUserQueryRepository queryRepo, IUserCommandRepository commandRepo)
        : ICommandHandler<DeleteUserRequest>
    {
        public async Task<Unit> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var userId = identityInfo.GetIdentityId();

            var user = await queryRepo.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                throw new NotFoundException(nameof(User), userId);

            await commandRepo.DeleteAsync(user, true, cancellationToken);

            return Unit.Value;
        }
    }
}
