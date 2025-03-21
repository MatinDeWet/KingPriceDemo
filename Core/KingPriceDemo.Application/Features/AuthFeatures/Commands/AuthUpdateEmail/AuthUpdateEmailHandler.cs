using KingPriceDemo.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthUpdateEmail
{
    public class AuthUpdateEmailHandler(IIdentityInfo identityInfo, UserManager<ApplicationUser> userManager)
        : ICommandHandler<AuthUpdateEmailRequest>
    {
        public async Task<Unit> Handle(AuthUpdateEmailRequest request, CancellationToken cancellationToken)
        {
            var userId = identityInfo.GetIdentityId();

            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                throw new NotFoundException(nameof(ApplicationUser), userId);

            user.Email = request.Email;
            user.UserName = request.Email;

            await userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
