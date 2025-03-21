using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthUpdateEmail;
using KingPriceDemo.Application.Repositories.CommandRepositories;
using KingPriceDemo.Application.Repositories.QueryRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.UserFeatures.Commands.UpdateUser
{
    public class UpdateUserHandler(ISender sender, IUserQueryRepository queryRepo, IUserCommandRepository commandRepo, IIdentityInfo identityInfo)
        : ICommandHandler<UpdateUserRequest>
    {
        public async Task<Unit> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var userId = identityInfo.GetIdentityId();

            var user = await queryRepo.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                throw new NotFoundException(nameof(User), userId);

            if (user.Email != request.Email)
                await sender.Send(new AuthUpdateEmailRequest(request.Email), cancellationToken);

            user.FullName = request.FullName;
            user.Surname = request.Surname;
            user.Email = request.Email;
            user.CellphoneNumber = request.CellphoneNumber;

            await commandRepo.UpdateAsync(user, true, cancellationToken);

            return Unit.Value;
        }
    }
}
