using KingPriceDemo.Application.Common.Constants;
using KingPriceDemo.Application.Features.AuthFeatures.Commands.GenerateAuthToken;
using KingPriceDemo.Application.Features.AuthFeatures.Commands.RefreshAuthToken;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.VerifyRefreshAuthToken
{
    public class VerifyRefreshAuthTokenHandler(UserManager<ApplicationUser> userManager, ISender sender)
            : ICommandHandler<VerifyRefreshAuthTokenRequest, VerifyRefreshAuthTokenResponse>
    {
        private ApplicationUser _user = null!;

        private const string _loginProvider = AuthConstants.LoginProvider;
        private const string _refreshToken = AuthConstants.RefreshToken;

        public async Task<VerifyRefreshAuthTokenResponse> Handle(VerifyRefreshAuthTokenRequest request, CancellationToken cancellationToken)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);

            var userId = tokenContent.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

            _user = await userManager.FindByIdAsync(userId) ?? null!;

            if (_user is null)
                return null!;

            var isValidRefreshToken = await userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await sender.Send(new GenerateAuthTokenRequest(_user));
                var refreshToken = await sender.Send(new RefreshAuthTokenRequest(_user));

                return new VerifyRefreshAuthTokenResponse(_user.Id, token, refreshToken);
            }

            await userManager.UpdateSecurityStampAsync(_user);

            return null!;
        }
    }
}
