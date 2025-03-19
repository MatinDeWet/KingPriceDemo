using KingPriceDemo.Application.Common.Constants;
using Microsoft.AspNetCore.Identity;

namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.RefreshAuthToken
{
    public class RefreshAuthTokenHandler(UserManager<ApplicationUser> userManager)
            : ICommandHandler<RefreshAuthTokenRequest, string>
    {
        private const string _loginProvider = AuthConstants.LoginProvider;
        private const string _refreshToken = AuthConstants.RefreshToken;

        public async Task<string> Handle(RefreshAuthTokenRequest request, CancellationToken cancellationToken)
        {
            await userManager.RemoveAuthenticationTokenAsync(request.user, _loginProvider, _refreshToken);

            var newRefreshToken = await userManager.GenerateUserTokenAsync(request.user, _loginProvider, _refreshToken);

            var result = await userManager.SetAuthenticationTokenAsync(request.user, _loginProvider!, _refreshToken, newRefreshToken);

            return newRefreshToken;
        }
    }
}
