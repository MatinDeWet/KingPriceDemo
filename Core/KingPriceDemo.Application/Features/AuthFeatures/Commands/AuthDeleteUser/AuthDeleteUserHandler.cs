using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Features.UserFeatures.Commands.DeleteUser;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthDeleteUser
{
    public class AuthDeleteUserHandler(ISender sender, IIdentityInfo identityInfo, UserManager<ApplicationUser> userManager)
        : ICommandHandler<AuthDeleteUserRequest>
    {
        public async Task<Unit> Handle(AuthDeleteUserRequest request, CancellationToken cancellationToken)
        {
            var userId = identityInfo.GetIdentityId();

            await sender.Send(new DeleteUserRequest(), cancellationToken);

            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                throw new NotFoundException(nameof(ApplicationUser), userId);

            await userManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
