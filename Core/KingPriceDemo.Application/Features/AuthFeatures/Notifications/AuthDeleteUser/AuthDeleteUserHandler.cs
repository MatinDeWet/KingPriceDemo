using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Domain.EventEntities;
using Microsoft.AspNetCore.Identity;

namespace KingPriceDemo.Application.Features.AuthFeatures.Notifications.AuthDeleteUser
{
    public class AuthDeleteUserHandler(IIdentityInfo identityInfo, UserManager<ApplicationUser> userManager)
        : IPriorityNotificationHandler<DeleteUserEvent>
    {
        public int Priority => 2;

        public async Task Handle(DeleteUserEvent request, CancellationToken cancellationToken)
        {
            var userId = identityInfo.GetIdentityId();

            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                throw new NotFoundException(nameof(ApplicationUser), userId);

            await userManager.DeleteAsync(user);
        }
    }
}
